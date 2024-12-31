using System;
namespace TCS.ProgramExamples.Blockchain {
    [Serializable]
    public class NodeManager {
        public Blockchain Blockchain;
        public Wallet NodeWallet;

        public NodeManager() {
            Blockchain = new Blockchain();
            NodeWallet = new Wallet(Blockchain);
        }

        public void ReceiveTransaction(Transaction tx) {
            Blockchain.AddTransaction(tx);
        }

        public void Mine() {
            Blockchain.MinePendingTransactions(NodeWallet.Address);
        }

        public float GetMyBalance() {
            return Blockchain.GetBalance(NodeWallet.Address);
        }
    }
}