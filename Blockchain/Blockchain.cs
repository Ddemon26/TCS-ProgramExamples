using System;
using System.Collections.Generic;
using UnityEngine;
namespace TCS.ProgramExamples.Blockchain {
    [Serializable]
    public class Blockchain {
        public List<Block> Chain = new();
        public List<Transaction> PendingTransactions = new();
        public const int DIFFICULTY = 2;
        public float MiningReward = 50f;
        public const int HALVING_INTERVAL = 210000;

        // The operational UTXO manager (final state)
        UtxoManager m_utxoManager = new();

        public Blockchain() {
            // Genesis block
            var genesis = new Block(0, "0");
            genesis.ComputeHash();
            Chain.Add(genesis);
        }

        public Block GetLatestBlock() => Chain[^1];

        public void AddTransaction(Transaction tx) {
            if (tx.m_outputs.Count == 0) {
                Debug.LogWarning("Attempted to add a transaction with no outputs.");
                return;
            }

            foreach (var o in tx.m_outputs) {

                if (!(o.m_amount <= 0f)) continue;
                Debug.LogWarning("Transaction output amount invalid.");
                return;
            }

            tx.ComputeHash();
            if (tx.m_fromAddress != "SYSTEM" && !tx.IsSignatureValid()) {
                Debug.LogWarning("Invalid signature on transaction, not adding to pending.");
                return;
            }

            PendingTransactions.Add(tx);
        }

        public void MinePendingTransactions(string minerAddress) {
            var block = new Block(Chain.Count, GetLatestBlock().m_hash);
            block.m_transactions.AddRange(PendingTransactions);
            PendingTransactions.Clear();

            // Coinbase transaction
            var coinbaseTx = new Transaction { m_fromAddress = "SYSTEM" };
            coinbaseTx.m_outputs.Add(new TransactionOutput { m_owner = minerAddress, m_amount = MiningReward });
            coinbaseTx.ComputeHash();
            block.m_transactions.Insert(0, coinbaseTx);

            // Proof of Work
            while (true) {
                block.ComputeHash();
                if (block.m_hash.StartsWith(new string('0', DIFFICULTY)))
                    break;
                block.m_nonce++;
            }

            if (!block.IsTimestampValid()) {
                Debug.LogWarning("Block timestamp invalid, aborting.");
                return;
            }

            Chain.Add(block);
            ApplyBlockTransactions(block);
            HandleHalvingSchedule();
        }

        void ApplyBlockTransactions(Block block) {
            // Update utxoManager state based on this block
            foreach (var tx in block.m_transactions) {
                if (tx.m_fromAddress != "SYSTEM") {
                    foreach (var inp in tx.m_inputs) {
                        m_utxoManager.SpendUtxo(inp.m_refTxHash, inp.m_outputIndex);
                    }
                }

                for (var i = 0; i < tx.m_outputs.Count; i++) {
                    var utxo = new Utxo {
                        m_txHash = tx.m_hash,
                        m_outputIndex = i,
                        m_owner = tx.m_outputs[i].m_owner,
                        m_amount = tx.m_outputs[i].m_amount
                    };
                    m_utxoManager.AddUtxo(utxo);
                }
            }
        }

        public float GetBalance(string address) {
            return m_utxoManager.GetBalance(address);
        }

        void HandleHalvingSchedule() {
            int height = Chain.Count - 1;
            int halvings = height / HALVING_INTERVAL;
            var baseReward = 50f;
            for (var i = 0; i < halvings; i++) {
                baseReward /= 2f;
                if (baseReward < 0.00000001f)
                    baseReward = 0.00000001f;
            }

            MiningReward = baseReward;
        }

        public Dictionary<string, Utxo> GetAllUtxos() {
            return m_utxoManager.Utxos;
        }

        public bool IsChainValid() {
            // Rebuild the chain from scratch into a temporary UTXO manager
            var tempUtxoManager = new UtxoManager();

            for (var i = 0; i < Chain.Count; i++) {
                var current = Chain[i];

                // Recompute hash and check integrity
                string originalHash = current.m_hash;
                current.ComputeHash();
                if (current.m_hash != originalHash) {
                    Debug.LogWarning("Block hash doesn't match computed hash.");
                    return false;
                }

                // Check previous hash consistency (except genesis)
                if (i > 0) {
                    var previous = Chain[i - 1];
                    if (current.m_previousHash != previous.m_hash) {
                        Debug.LogWarning("Block previous hash doesn't match.");
                        return false;
                    }
                }

                // Check timestamp
                if (!current.IsTimestampValid()) {
                    Debug.LogWarning("Invalid block timestamp detected.");
                    return false;
                }

                // Validate transactions in current block
                foreach (var tx in current.m_transactions) {
                    tx.ComputeHash();
                    if (!ValidateTransaction(tx, tempUtxoManager)) {
                        return false;
                    }

                    // Apply to temp utxos
                    if (tx.m_fromAddress != "SYSTEM") {
                        foreach (var inp in tx.m_inputs) {
                            bool spent = tempUtxoManager.SpendUtxo(inp.m_refTxHash, inp.m_outputIndex);

                            if (spent) continue;
                            Debug.LogWarning("Transaction references nonexistent UTXOs in validation phase!");
                            return false;
                        }
                    }

                    // Add new outputs
                    for (var outIndex = 0; outIndex < tx.m_outputs.Count; outIndex++) {
                        var newUtxo = new Utxo {
                            m_txHash = tx.m_hash,
                            m_outputIndex = outIndex,
                            m_owner = tx.m_outputs[outIndex].m_owner,
                            m_amount = tx.m_outputs[outIndex].m_amount
                        };
                        tempUtxoManager.AddUtxo(newUtxo);
                    }
                }
            }

            return true;
        }

        static bool ValidateTransaction(Transaction tx, UtxoManager tempUtxoManager) {
            if (!tx.IsSignatureValid()) {
                Debug.LogWarning("Invalid transaction signature during validation!");
                return false;
            }

            float inputSum = 0;
            if (tx.m_fromAddress != "SYSTEM") {
                // Check inputs exist in temp utxos
                foreach (var inp in tx.m_inputs) {
                    string key = inp.m_refTxHash + "-" + inp.m_outputIndex;
                    if (!tempUtxoManager.Utxos.TryGetValue(key, out var utxo)) {
                        Debug.LogWarning("Transaction input UTXO does not exist in validation!");
                        return false;
                    }

                    inputSum += utxo.m_amount;
                }
            }

            float outputSum = tx.GetTotalOutput();

            if (tx.m_fromAddress != "SYSTEM") {
                if (inputSum < outputSum) {
                    Debug.LogWarning("Transaction spends more than input in validation!");
                    return false;
                }
            }

            return true;
        }
    }
}