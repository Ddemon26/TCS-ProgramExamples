using UnityEngine;
namespace TCS.ProgramExamples.MatrixExamples {
    public class BspNode {
        public Rect Rect;
        public Rect Room;
        public BspNode Left;
        public BspNode Right;

        public BspNode(Rect rect) {
            Rect = rect;
            Room = new Rect();
        }
    }
}