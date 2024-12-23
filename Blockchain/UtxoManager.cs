using System.Collections.Generic;
namespace TCS.ProgramExamples.Blockchain {
    public class UtxoManager {
        public readonly Dictionary<string, Utxo> Utxos = new();

        public void AddUtxo(Utxo utxo) {
            string key = utxo.m_txHash + "-" + utxo.m_outputIndex;
            Utxos[key] = utxo;
        }

        public bool SpendUtxo(string txHash, int outputIndex) {
            string key = txHash + "-" + outputIndex;
            return Utxos.Remove(key);
        }

        public float GetBalance(string address) {
            var sum = 0f;
            foreach (KeyValuePair<string, Utxo> kvp in Utxos) {
                if (kvp.Value.m_owner == address)
                    sum += kvp.Value.m_amount;
            }

            return sum;
        }
    }
}