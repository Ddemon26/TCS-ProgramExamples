using System.Collections.Generic;
using UnityEngine;
namespace TCS.ProgramExamples.MatrixExamples {
    public class BspDungeonGenerator : MonoBehaviour {
        public int m_dungeonWidth = 50;
        public int m_dungeonHeight = 50;
        public int m_minRoomSize = 5;
        public int m_maxRoomSize = 15;
        public int m_maxIterations = 5;
        
        public List<Rect> m_rooms = new();

        /// <summary>
        /// Generates the dungeon using BSP and populates the RoomMatrixManager.
        /// </summary>
        public void GenerateDungeon(RoomMatrixManager roomMatrixManager) {
            m_rooms.Clear();
            var rootNode = new BspNode(new Rect(0, 0, m_dungeonWidth, m_dungeonHeight));
            SplitNode(rootNode, m_maxIterations);
            CreateRooms(rootNode);
            CreateCorridors(rootNode);
            PopulateRoomMatrix(roomMatrixManager);
        }

        /// <summary>
        /// Splits the node into two child nodes recursively to create a BSP tree.
        /// </summary>
        void SplitNode(BspNode node, int iterations) {
            while (true) {
                if (iterations <= 0 || (node.Rect.width <= m_minRoomSize * 2 && node.Rect.height <= m_minRoomSize * 2)) return;

                bool splitHorizontally = Random.value > 0.5f;

                if (node.Rect.width > node.Rect.height && node.Rect.width / node.Rect.height >= 1.25f) splitHorizontally = false;
                else if (node.Rect.height > node.Rect.width && node.Rect.height / node.Rect.width >= 1.25f) splitHorizontally = true;

                if (splitHorizontally) {
                    int splitY = Random.Range(m_minRoomSize, (int)(node.Rect.height - m_minRoomSize));
                    node.Left = new BspNode(new Rect(node.Rect.x, node.Rect.y, node.Rect.width, splitY));
                    node.Right = new BspNode(new Rect(node.Rect.x, node.Rect.y + splitY, node.Rect.width, node.Rect.height - splitY));
                }
                else {
                    int splitX = Random.Range(m_minRoomSize, (int)(node.Rect.width - m_minRoomSize));
                    node.Left = new BspNode(new Rect(node.Rect.x, node.Rect.y, splitX, node.Rect.height));
                    node.Right = new BspNode(new Rect(node.Rect.x + splitX, node.Rect.y, node.Rect.width - splitX, node.Rect.height));
                }

                SplitNode(node.Left, iterations - 1);
                node = node.Right;
                iterations = iterations - 1;
            }
        }

        /// <summary>
        /// Creates rooms within the leaf nodes of the BSP tree.
        /// </summary>
        void CreateRooms(BspNode node) {
            while (true) {
                if (node.Left == null && node.Right == null) {
                    int roomWidth = Random.Range(m_minRoomSize, (int)Mathf.Min(m_maxRoomSize, node.Rect.width));
                    int roomHeight = Random.Range(m_minRoomSize, (int)Mathf.Min(m_maxRoomSize, node.Rect.height));
                    var roomX = (int)(node.Rect.x + Random.Range(0, node.Rect.width - roomWidth));
                    var roomY = (int)(node.Rect.y + Random.Range(0, node.Rect.height - roomHeight));

                    node.Room = new Rect(roomX, roomY, roomWidth, roomHeight);
                    m_rooms.Add(node.Room);
                }
                else {
                    if (node.Left != null) CreateRooms(node.Left);
                    if (node.Right != null) {
                        node = node.Right;
                        continue;
                    }
                }

                break;
            }
        }

        /// <summary>
        /// Creates corridors between rooms in the BSP tree.
        /// </summary>
        void CreateCorridors(BspNode node) {
            while (true) {
                if (node.Left == null || node.Right == null) return;

                var leftRoom = GetRoomRect(node.Left);
                var rightRoom = GetRoomRect(node.Right);

                var leftPoint = new Vector2(leftRoom.x + leftRoom.width / 2, leftRoom.y + leftRoom.height / 2);
                var rightPoint = new Vector2(rightRoom.x + rightRoom.width / 2, rightRoom.y + rightRoom.height / 2);

                // Create a corridor between leftPoint and rightPoint (can be L-shaped)
                // TODO: Implement corridor creation logic if needed

                CreateCorridors(node.Left);
                node = node.Right;
            }
        }

        static Rect GetRoomRect(BspNode node) {
            if (node.Room.width > 0 && node.Room.height > 0)
                return node.Room;
            else {
                var leftRoom = new Rect();
                var rightRoom = new Rect();
                if (node.Left != null)
                    leftRoom = GetRoomRect(node.Left);
                if (node.Right != null)
                    rightRoom = GetRoomRect(node.Right);

                if (leftRoom.width == 0 || leftRoom.height == 0)
                    return rightRoom;
                else if (rightRoom.width == 0 || rightRoom.height == 0)
                    return leftRoom;
                else
                    return Random.value > 0.5f ? leftRoom : rightRoom;
            }
        }

        /// <summary>
        /// Populates the RoomMatrixManager with rooms and sets room types.
        /// </summary>
        public void PopulateRoomMatrix(RoomMatrixManager roomMatrixManager) {
            roomMatrixManager.m_rows = m_dungeonHeight;
            roomMatrixManager.m_columns = m_dungeonWidth;
            roomMatrixManager.GenerateRooms();

            foreach (var room in m_rooms) {
                for (int x = Mathf.FloorToInt(room.x); x < Mathf.CeilToInt(room.xMax); x++) {
                    for (int y = Mathf.FloorToInt(room.y); y < Mathf.CeilToInt(room.yMax); y++) {
                        // Note: Adjusting x and y to match the matrix indices
                        if (roomMatrixManager.IsValidPosition(y, x)) {
                            roomMatrixManager.SetRoomType(y, x, RoomType.Special);
                            // Additional logic to set room properties can be added here
                        }
                    }
                }
            }

            // Optionally, generate corridors between rooms and update doors in roomMatrixManager
        }
    }
}