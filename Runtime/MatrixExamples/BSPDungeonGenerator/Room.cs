using UnityEngine;
namespace TCS.ProgramExamples.MatrixExamples {
    [System.Serializable]
    public class Room {
        public Vector2Int Position { get; private set; }
        public RoomType m_roomType;
        public bool m_isVisited;
        public bool m_hasEnemy;
        public bool m_hasTreasure;
        public bool m_hasTrap;

        // Connectivity properties
        public bool m_hasNorthDoor;
        public bool m_hasSouthDoor;
        public bool m_hasEastDoor;
        public bool m_hasWestDoor;

        public Room(Vector2Int position, RoomType roomType) {
            Position = position;
            m_roomType = roomType;
            m_isVisited = false;
            m_hasEnemy = false;
            m_hasTreasure = false;
            m_hasTrap = false;

            // Initialize doors (will be set in GenerateDoors)
            m_hasNorthDoor = false;
            m_hasSouthDoor = false;
            m_hasEastDoor = false;
            m_hasWestDoor = false;
        }
    }
}