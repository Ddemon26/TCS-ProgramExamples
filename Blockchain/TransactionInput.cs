using System;
namespace TCS.ProgramExamples.Blockchain {
    [Serializable] public class TransactionInput {
        public string m_refTxHash;
        public int m_outputIndex;
    }
}