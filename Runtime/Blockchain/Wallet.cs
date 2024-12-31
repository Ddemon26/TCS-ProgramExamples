using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
namespace TCS.ProgramExamples.Blockchain {
    [Serializable]
    public class Wallet {
        public byte[] PrivateKey;
        public byte[] PublicKey;
        public string Address;

        public readonly Blockchain ConnectedBlockchain;

        public Wallet(Blockchain chain) {
            (byte[] privateKey, byte[] publicKey) keys = CryptoUtils.GenerateRsaKeyPair();
            PrivateKey = keys.privateKey;
            PublicKey = keys.publicKey;
            Address = CryptoUtils.ComputeSHA256(Convert.ToBase64String(PublicKey));
            ConnectedBlockchain = chain;
        }

        public Transaction CreateTransaction(string toAddress, float amount) {
            float balance = ConnectedBlockchain.GetBalance(Address);
            if (balance < amount) {
                Debug.LogWarning("Insufficient balance to create transaction.");
                return null;
            }

            // Gather UTXOs sufficient to cover 'amount'
            List<(string, int, float)> selectedUtxos = new();
            var accumulated = 0f;
            foreach (KeyValuePair<string, Utxo> kvp in ConnectedBlockchain.GetAllUtxos()) {
                var utxo = kvp.Value;
                if (utxo.m_owner == Address) {
                    selectedUtxos.Add((utxo.m_txHash, utxo.m_outputIndex, utxo.m_amount));
                    accumulated += utxo.m_amount;
                    if (accumulated >= amount)
                        break;
                }
            }

            if (accumulated < amount) {
                Debug.LogWarning("Failed to gather enough UTXOs for transaction.");
                return null;
            }

            var tx = new Transaction {
                m_fromAddress = Address,
                m_fromPublicKey = PublicKey
            };

            // Inputs
            foreach (var s in selectedUtxos) {
                tx.m_inputs.Add
                (
                    new TransactionInput {
                        m_refTxHash = s.Item1,
                        m_outputIndex = s.Item2
                    }
                );
            }

            // Outputs
            tx.m_outputs.Add
            (
                new TransactionOutput {
                    m_owner = toAddress,
                    m_amount = amount
                }
            );

            float change = accumulated - amount;
            if (change > 0) {
                tx.m_outputs.Add
                (
                    new TransactionOutput {
                        m_owner = Address,
                        m_amount = change
                    }
                );
            }

            tx.ComputeHash();
            byte[] data = Encoding.UTF8.GetBytes(tx.m_hash);
            tx.m_signature = CryptoUtils.SignDataRsa(data, PrivateKey);

            return tx;
        }
    }
}