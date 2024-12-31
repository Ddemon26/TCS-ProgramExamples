using UnityEditor;
using UnityEngine;
namespace TCS.ProgramExamples.MatrixExamples {
    #region Custom Editor (Must be in 'Editor' Folder)
#if UNITY_EDITOR
    [CustomEditor(typeof(RoomMatrixManager))]
    public class RoomMatrixManagerEditor : Editor {
        SerializedProperty m_rowsProp;
        SerializedProperty m_columnsProp;
        RoomMatrixManager m_roomMatrixManager;

        void OnEnable() {
            m_rowsProp = serializedObject.FindProperty("rows");
            m_columnsProp = serializedObject.FindProperty("columns");
            m_roomMatrixManager = (RoomMatrixManager)target;

            if (m_roomMatrixManager.m_roomMatrix != null && m_roomMatrixManager.m_roomMatrix.Count == m_roomMatrixManager.m_rows) return;
            m_roomMatrixManager.GenerateRooms();
            EditorUtility.SetDirty(m_roomMatrixManager);
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            // Error checking for large matrices
            if (m_rowsProp.intValue * m_columnsProp.intValue > 100) {
                EditorGUILayout.HelpBox("Matrix size is too large and may affect performance.", MessageType.Warning);
            }

            // Row and Column Controls
            EditorGUILayout.PropertyField(m_rowsProp, new GUIContent("Rows"));
            EditorGUILayout.PropertyField(m_columnsProp, new GUIContent("Columns"));

            if (GUILayout.Button("Generate Rooms")) {
                m_roomMatrixManager.GenerateRooms();
                EditorUtility.SetDirty(m_roomMatrixManager);
            }

            serializedObject.ApplyModifiedProperties();

            // Display the Room Matrix with paging
            const int pageSize = 10; // Number of rows per page
            int totalRows = m_roomMatrixManager.m_roomMatrix.Count;
            int totalPages = Mathf.CeilToInt((float)totalRows / pageSize);
            m_roomMatrixManager.m_currentPage = Mathf.Clamp(m_roomMatrixManager.m_currentPage, 0, totalPages - 1);

            GUILayout.Space(10);
            GUILayout.Label($"Room Matrix - Page {m_roomMatrixManager.m_currentPage + 1} of {totalPages}", EditorStyles.boldLabel);

            // Paging Controls
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Previous Page")) {
                m_roomMatrixManager.m_currentPage = Mathf.Max(m_roomMatrixManager.m_currentPage - 1, 0);
            }

            if (GUILayout.Button("Next Page")) {
                m_roomMatrixManager.m_currentPage = Mathf.Min(m_roomMatrixManager.m_currentPage + 1, totalPages - 1);
            }

            GUILayout.EndHorizontal();

            int startRow = m_roomMatrixManager.m_currentPage * pageSize;
            int endRow = Mathf.Min(startRow + pageSize, totalRows);

            // Display the paged room matrix
            for (int row = startRow; row < endRow; row++) {
                GUILayout.BeginHorizontal();
                for (var col = 0; col < m_roomMatrixManager.m_roomMatrix[row].m_rooms.Count; col++) {
                    var room = m_roomMatrixManager.m_roomMatrix[row].m_rooms[col];
                    if (room == null) continue;

                    DrawRoomButton(room, row, col);
                }

                GUILayout.EndHorizontal();
            }
        }

        void DrawRoomButton(Room room, int row, int col) {
            var originalColor = GUI.backgroundColor;

            GUI.backgroundColor = GetRoomColor(room.m_roomType);
            var tooltip = $"Type: {room.m_roomType}\nEnemy: {room.m_hasEnemy}\nTreasure: {room.m_hasTreasure}\nTrap: {room.m_hasTrap}";
            var content = new GUIContent(room.m_roomType.ToString().Substring(0, 1), tooltip);

            if (GUILayout.Button(content, GUILayout.Width(30), GUILayout.Height(30))) {
                ShowRoomContextMenu(row, col, room);
            }

            GUI.backgroundColor = originalColor;
        }

        static Color GetRoomColor(RoomType roomType) {
            return roomType switch {
                RoomType.Common => Color.gray,
                RoomType.Special => Color.blue,
                RoomType.Treasure => Color.yellow,
                RoomType.Trap => Color.red,
                RoomType.Enemy => Color.magenta,
                _ => Color.white,
            };
        }

        void ShowRoomContextMenu(int row, int col, Room room) {
            var menu = new GenericMenu();

            foreach (RoomType roomType in System.Enum.GetValues(typeof(RoomType))) {
                menu.AddItem
                (
                    new GUIContent($"Set Room Type/{roomType}"), room.m_roomType == roomType, () => {
                        m_roomMatrixManager.SetRoomType(row, col, roomType);
                        EditorUtility.SetDirty(m_roomMatrixManager);
                    }
                );
            }

            menu.AddSeparator("");

            menu.AddItem
            (
                new GUIContent("Toggle Enemy"), room.m_hasEnemy, () => {
                    room.m_hasEnemy = !room.m_hasEnemy;
                    EditorUtility.SetDirty(m_roomMatrixManager);
                }
            );

            menu.AddItem
            (
                new GUIContent("Toggle Treasure"), room.m_hasTreasure, () => {
                    room.m_hasTreasure = !room.m_hasTreasure;
                    EditorUtility.SetDirty(m_roomMatrixManager);
                }
            );

            menu.AddItem
            (
                new GUIContent("Toggle Trap"), room.m_hasTrap, () => {
                    room.m_hasTrap = !room.m_hasTrap;
                    EditorUtility.SetDirty(m_roomMatrixManager);
                }
            );

            menu.AddSeparator("");

            menu.AddItem
            (
                new GUIContent("Toggle Doors/North Door"), room.m_hasNorthDoor, () => {
                    room.m_hasNorthDoor = !room.m_hasNorthDoor;
                    EditorUtility.SetDirty(m_roomMatrixManager);
                }
            );

            menu.AddItem
            (
                new GUIContent("Toggle Doors/South Door"), room.m_hasSouthDoor, () => {
                    room.m_hasSouthDoor = !room.m_hasSouthDoor;
                    EditorUtility.SetDirty(m_roomMatrixManager);
                }
            );

            menu.AddItem
            (
                new GUIContent("Toggle Doors/East Door"), room.m_hasEastDoor, () => {
                    room.m_hasEastDoor = !room.m_hasEastDoor;
                    EditorUtility.SetDirty(m_roomMatrixManager);
                }
            );

            menu.AddItem
            (
                new GUIContent("Toggle Doors/West Door"), room.m_hasWestDoor, () => {
                    room.m_hasWestDoor = !room.m_hasWestDoor;
                    EditorUtility.SetDirty(m_roomMatrixManager);
                }
            );

            menu.ShowAsContext();
        }
    }
#endif
    #endregion
}