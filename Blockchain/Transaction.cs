using System;
using System.Collections.Generic;
using System.Text;
namespace TCS.ProgramExamples.Blockchain {
    [Serializable] public class Transaction {
        public string m_fromAddress;
        public byte[] m_signature;
        public byte[] m_fromPublicKey;

        public List<TransactionInput> m_inputs = new();
        public List<TransactionOutput> m_outputs = new();

        public string m_hash;

        public void ComputeHash() {
            var sb = new StringBuilder();
            sb.Append(m_fromAddress);
            foreach (var input in m_inputs) {
                sb.Append(input.m_refTxHash);
                sb.Append(input.m_outputIndex);
            }

            foreach (var output in m_outputs) {
                sb.Append(output.m_owner);
                sb.Append(output.m_amount.ToString("F8"));
            }

            m_hash = CryptoUtils.ComputeSHA256(sb.ToString());
        }

        public bool IsSignatureValid() {
            if (m_fromAddress == "SYSTEM") return true; // coinbase doesn't need signature
            if (m_fromPublicKey == null || m_signature == null || m_signature.Length == 0)
                return false;

            byte[] data = Encoding.UTF8.GetBytes(m_hash);
            return CryptoUtils.VerifyDataRsa(data, m_signature, m_fromPublicKey);
        }

        public float GetTotalOutput() {
            float sum = 0;

            foreach (var o in m_outputs) {
                sum += o.m_amount;
            }

            return sum;
        }

        public float GetTotalInputAmount(UtxoManager utxoManager) {
            float sum = 0;
            foreach (var inp in m_inputs) {
                string key = inp.m_refTxHash + "-" + inp.m_outputIndex;
                if (!utxoManager.Utxos.TryGetValue(key, out var utxo)) {
                    // Referenced UTXO doesn't exist
                    return -1;
                }

                sum += utxo.m_amount;
            }

            return sum;
        }
    }
}