// Namespaces
using UnityEngine;
namespace TCS.ProgramExamples.Blockchain {
    public class BlockchainManager : MonoBehaviour {
        public NodeManager m_localNode;
        public Wallet m_playerWallet;

        void Start() {
            m_localNode = new NodeManager();
            m_playerWallet = new Wallet(m_localNode.Blockchain);

            Debug.Log("Local Node Address: " + m_localNode.NodeWallet.Address);
            Debug.Log("Player Address: " + m_playerWallet.Address);

            // Give player initial funds
            var initialTx = new Transaction { m_fromAddress = "SYSTEM" };
            initialTx.m_outputs.Add(new TransactionOutput { m_owner = m_playerWallet.Address, m_amount = 50f });
            initialTx.ComputeHash();
            m_localNode.Blockchain.PendingTransactions.Add(initialTx);

            // Mine initial transaction
            m_localNode.Mine();
            Debug.Log("Mined initial tx. Player Balance: " + m_localNode.Blockchain.GetBalance(m_playerWallet.Address));

            // Player sends 10 to Node
            var playerToNodeTx = m_playerWallet.CreateTransaction(m_localNode.NodeWallet.Address, 10f);
            if (playerToNodeTx != null) {
                m_localNode.ReceiveTransaction(playerToNodeTx);
                m_localNode.Mine();

                Debug.Log("After Player->Node Tx:");
                Debug.Log("Player Balance: " + m_localNode.Blockchain.GetBalance(m_playerWallet.Address));
                Debug.Log("Node Balance: " + m_localNode.Blockchain.GetBalance(m_localNode.NodeWallet.Address));
            }
            else {
                Debug.LogWarning("Failed to create Player->Node transaction.");
            }

            bool isValid = m_localNode.Blockchain.IsChainValid();
            Debug.Log("Blockchain valid: " + isValid);

            float balance = m_localNode.GetMyBalance();
            Debug.Log("Node balance: " + balance);
        }
    }
}