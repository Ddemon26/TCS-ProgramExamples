using System;
using System.Collections.Generic;
using System.Text;
namespace TCS.ProgramExamples.Blockchain {
    [Serializable] public class Block {
        public int m_index;
        public long m_timestamp;
        public List<Transaction> m_transactions = new();
        public string m_previousHash;
        public string m_hash;
        public int m_nonce;

        public Block(int index, string previousHash) {
            m_index = index;
            m_previousHash = previousHash;
            m_timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        public void ComputeHash() {
            string merkleRoot = ComputeMerkleRoot();
            string blockData = m_index + m_timestamp.ToString() + m_previousHash + merkleRoot + m_nonce;
            m_hash = CryptoUtils.ComputeSHA256(blockData);
        }

        public string ComputeMerkleRoot() {
            var sb = new StringBuilder();
            foreach (var tx in m_transactions) {
                tx.ComputeHash();
                sb.Append(tx.m_hash);
            }

            return CryptoUtils.ComputeSHA256(sb.ToString());
        }

        public bool IsTimestampValid() {
            long now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            return m_timestamp <= now + 7200;
        }
    }
}