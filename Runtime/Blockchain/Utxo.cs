using System;
namespace TCS.ProgramExamples.Blockchain {
    [Serializable] public class Utxo {
        public string m_txHash;
        public int m_outputIndex;
        public string m_owner;
        public float m_amount;
    }
}