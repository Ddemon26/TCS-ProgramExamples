using System.Collections.Generic;
using UnityEngine;
namespace TCS.ProgramExamples.MatrixExamples {
    [ExecuteInEditMode]
    public class RoomMatrixManager : MonoBehaviour {
        [SerializeField, Min(1)]
        public int m_rows = 5;
        [SerializeField, Min(1)]
        public int m_columns = 5;
        public List<RoomRow> m_roomMatrix = new();

        // For paging in the custom editor
        [HideInInspector]
        public int m_currentPage;

        // Prefabs for room visualization (optional)
        public GameObject m_commonRoomPrefab;
        public GameObject m_specialRoomPrefab;
        public GameObject m_treasureRoomPrefab;
        public GameObject m_trapRoomPrefab;
        public GameObject m_enemyRoomPrefab;

        /// <summary>
        /// Generates the room matrix and initializes rooms.
        /// </summary>
        public void GenerateRooms() {
            m_roomMatrix.Clear();

            // Define probabilities for each room type
            Dictionary<RoomType, float> roomTypeProbabilities = new() {
                { RoomType.Common, 0.5f },
                { RoomType.Special, 0.2f },
                { RoomType.Treasure, 0.1f },
                { RoomType.Trap, 0.1f },
                { RoomType.Enemy, 0.1f }
            };

            for (var row = 0; row < m_rows; row++) {
                var newRow = new RoomRow();
                for (var col = 0; col < m_columns; col++) {
                    var roomType = GetRandomRoomType(roomTypeProbabilities);
                    newRow.m_rooms.Add(new Room(new Vector2Int(row, col), roomType));
                }

                m_roomMatrix.Add(newRow);
            }

            // Automatically generate doors between adjacent rooms
            GenerateDoors();

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        /// <summary>
        /// Automatically generates doors between adjacent rooms to ensure connectivity.
        /// </summary>
        public void GenerateDoors() {
            for (var row = 0; row < m_rows; row++) {
                for (var col = 0; col < m_columns; col++) {
                    var room = m_roomMatrix[row].m_rooms[col];

                    // East door (connect to room on the right)
                    if (IsValidPosition(row, col + 1)) {
                        var hasDoor = true; // Or randomize with Random.value > 0.5f
                        room.m_hasEastDoor = hasDoor;
                        m_roomMatrix[row].m_rooms[col + 1].m_hasWestDoor = hasDoor;
                    }

                    // South door (connect to room below)
                    if (IsValidPosition(row + 1, col)) {
                        var hasDoor = true; // Or randomize with Random.value > 0.5f
                        room.m_hasSouthDoor = hasDoor;
                        m_roomMatrix[row + 1].m_rooms[col].m_hasNorthDoor = hasDoor;
                    }
                }
            }
        }

        /// <summary>
        /// Gets a random room type based on defined probabilities.
        /// </summary>
        static RoomType GetRandomRoomType(Dictionary<RoomType, float> probabilities) {
            float randomValue = Random.value;
            var cumulative = 0f;
            foreach (KeyValuePair<RoomType, float> kvp in probabilities) {
                cumulative += kvp.Value;
                if (randomValue <= cumulative)
                    return kvp.Key;
            }

            return RoomType.Common; // Default fallback
        }

        /// <summary>
        /// Sets the room type at the specified position.
        /// </summary>
        public void SetRoomType(int row, int col, RoomType roomType) {
            if (!IsValidPosition(row, col)) return;

            m_roomMatrix[row].m_rooms[col].m_roomType = roomType;
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        /// <summary>
        /// Checks if the given position is valid within the matrix.
        /// </summary>
        public bool IsValidPosition(int row, int col) => row >= 0 && row < m_rows && col >= 0 && col < m_columns;

        /// <summary>
        /// Visualizes the room matrix in the Scene view using Gizmos.
        /// </summary>
        void OnDrawGizmos() {
            if (m_roomMatrix == null || m_roomMatrix.Count == 0) return;

            const float roomSize = 1.0f;
            var offset = new Vector3(-m_columns * roomSize * 0.5f, 0, -m_rows * roomSize * 0.5f);

            for (var row = 0; row < m_roomMatrix.Count; row++) {
                for (var col = 0; col < m_roomMatrix[row].m_rooms.Count; col++) {
                    var room = m_roomMatrix[row].m_rooms[col];
                    var position = new Vector3(col * roomSize, 0, row * roomSize) + offset + transform.position;

                    // Set Gizmo color based on room type
                    Gizmos.color = room.m_roomType switch {
                        RoomType.Common => Color.gray,
                        RoomType.Special => Color.blue,
                        RoomType.Treasure => Color.yellow,
                        RoomType.Trap => Color.red,
                        RoomType.Enemy => Color.magenta,
                        _ => Gizmos.color
                    };

                    Gizmos.DrawCube(position, Vector3.one * 0.9f);

                    // Draw doors
                    Gizmos.color = Color.white;
                    const float doorSize = 0.2f;
                    if (room.m_hasNorthDoor) {
                        var doorPos = position + new Vector3(0, 0, roomSize * 0.45f);
                        Gizmos.DrawCube(doorPos, new Vector3(doorSize, doorSize, 0.1f));
                    }

                    if (room.m_hasSouthDoor) {
                        var doorPos = position + new Vector3(0, 0, -roomSize * 0.45f);
                        Gizmos.DrawCube(doorPos, new Vector3(doorSize, doorSize, 0.1f));
                    }

                    if (room.m_hasEastDoor) {
                        var doorPos = position + new Vector3(roomSize * 0.45f, 0, 0);
                        Gizmos.DrawCube(doorPos, new Vector3(0.1f, doorSize, doorSize));
                    }

                    if (room.m_hasWestDoor) {
                        var doorPos = position + new Vector3(-roomSize * 0.45f, 0, 0);
                        Gizmos.DrawCube(doorPos, new Vector3(0.1f, doorSize, doorSize));
                    }
                }
            }
        }

        #region Pathfinding Methods
        /// <summary>
        /// Finds a path from the start room to the target room using A* algorithm.
        /// </summary>
        public List<Room> FindPath(Room startRoom, Room targetRoom) {
            List<Room> openSet = new();
            HashSet<Room> closedSet = new();
            openSet.Add(startRoom);

            Dictionary<Room, Room> cameFrom = new();
            Dictionary<Room, float> gScore = new();
            Dictionary<Room, float> fScore = new();

            gScore[startRoom] = 0;
            fScore[startRoom] = GetHeuristic(startRoom, targetRoom);

            while (openSet.Count > 0) {
                var current = GetRoomWithLowestFScore(openSet, fScore);

                if (current == null)
                    break;

                if (current == targetRoom) {
                    return ReconstructPath(cameFrom, current);
                }

                openSet.Remove(current);
                closedSet.Add(current);

                foreach (var neighbor in GetNeighbors(current)) {
                    if (closedSet.Contains(neighbor))
                        continue;

                    float tentativeGScore = gScore[current] + 1; // Assuming uniform cost

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                    else if (gScore.ContainsKey(neighbor) && tentativeGScore >= gScore[neighbor])
                        continue;

                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + GetHeuristic(neighbor, targetRoom);
                }
            }

            // No path found
            return null;
        }

        float GetHeuristic(Room a, Room b) {
            return Vector2Int.Distance(a.Position, b.Position);
        }

        static Room GetRoomWithLowestFScore(List<Room> rooms, Dictionary<Room, float> fScore) {
            if (rooms == null || rooms.Count == 0)
                return null;

            var lowestRoom = rooms[0];
            float lowestScore = fScore.GetValueOrDefault(lowestRoom, float.MaxValue);

            foreach (var room in rooms) {
                if (room == null) continue;
                float score = fScore.GetValueOrDefault(room, float.MaxValue);
                if (score < lowestScore) {
                    lowestScore = score;
                    lowestRoom = room;
                }
            }

            return lowestRoom;
        }

        List<Room> GetNeighbors(Room room) {
            List<Room> neighbors = new();
            int row = room.Position.x;
            int col = room.Position.y;

            // Check for doors to determine connectivity
            if (room.m_hasNorthDoor && IsValidPosition(row - 1, col))
                neighbors.Add(m_roomMatrix[row - 1].m_rooms[col]);
            if (room.m_hasSouthDoor && IsValidPosition(row + 1, col))
                neighbors.Add(m_roomMatrix[row + 1].m_rooms[col]);
            if (room.m_hasWestDoor && IsValidPosition(row, col - 1))
                neighbors.Add(m_roomMatrix[row].m_rooms[col - 1]);
            if (room.m_hasEastDoor && IsValidPosition(row, col + 1))
                neighbors.Add(m_roomMatrix[row].m_rooms[col + 1]);

            return neighbors;
        }

        static List<Room> ReconstructPath(Dictionary<Room, Room> cameFrom, Room current) {
            List<Room> totalPath = new() { current };
            while (cameFrom.ContainsKey(current)) {
                current = cameFrom[current];
                totalPath.Insert(0, current);
            }

            return totalPath;
        }
        #endregion
    }
}