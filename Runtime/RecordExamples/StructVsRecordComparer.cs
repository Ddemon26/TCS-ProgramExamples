using System;
using UnityEngine;

namespace TCS {
    public class StructVsRecordComparer : MonoBehaviour {
        // ==== Struct Example ====
        // Structs are value types and are copied by value.
        // Useful for small, immutable data types.
        // Keep under 16 bytes
        public struct PlayerStatsStruct {
            public int Health { get; } // Read-only properties for immutability
            public int AttackPower { get; }

            public PlayerStatsStruct(int health, int attackPower) {
                Health = health;
                AttackPower = attackPower;
            }

            // Overriding Equals method
            public override bool Equals(object obj) {
                if (obj is PlayerStatsStruct other) {
                    return Health == other.Health && AttackPower == other.AttackPower;
                }

                return false;
            }

            // Overriding GetHashCode method
            public override int GetHashCode() => HashCode.Combine(Health, AttackPower);

            // Overriding ToString method
            public override string ToString() => $"PlayerStatsStruct(Health: {Health}, AttackPower: {AttackPower})";

            // Implementing Deconstruct method for deconstruction
            public void Deconstruct(out int health, out int attackPower) {
                health = Health;
                attackPower = AttackPower;
            }
        }

        // ==== Record Class Example ====
        // Record classes are reference types with built-in methods.
        // They are immutable by default and provide value-based equality.
        public record PlayerStatsRecordClass(int Health, int AttackPower);

        // ==== Mutable Record Class Example ====
        // Mutable records are reference types with mutable properties.
        // **Not recommended** due to potential side effects and complexities.
        // This should just be a normal class if mutability is required.
        public record MutablePlayerStatsRecordClass(int Health, int AttackPower) {
            public int Health { get; set; } = Health;
            public int AttackPower { get; set; } = AttackPower;

            public override string ToString() => $"MutablePlayerStatsRecordClass(Health: {Health}, AttackPower: {AttackPower})";

            // Implementing Deconstruct method for deconstruction
            public void Deconstruct(out int health, out int attackPower) {
                health = Health;
                attackPower = AttackPower;
            }
        }

        void Start() {
            // ==== Struct Example ====
            Debug.Log("=== Struct Example ===");

            var struct1 = new PlayerStatsStruct(100, 50);
            var struct2 = struct1; // Copy by value; struct2 is a separate copy

            Debug.Log($"[Struct] Original struct1: {struct1}\n- This is the original struct instance.");
            Debug.Log($"[Struct] Copied struct2: {struct2}\n- This is a copy of struct1.");

            // Modify struct2
            struct2 = new PlayerStatsStruct(120, 60);

            Debug.Log($"[Struct] After modifying struct2:\nstruct1 (unchanged): {struct1}\nstruct2 (modified): {struct2}");

            // Compare structs
            bool structsAreEqual = struct1.Equals(struct2);
            Debug.Log($"[Struct] Are struct1 and struct2 equal? {structsAreEqual}\n- They are {(structsAreEqual ? "equal" : "not equal")} because their values {(structsAreEqual ? "match" : "differ")}.");

            // Deconstruct struct
            (int structHealth, int structAttackPower) = struct1;
            Debug.Log($"[Struct] Deconstructed struct1: Health = {structHealth}, AttackPower = {structAttackPower}");

            // ==== Record Class Example ====
            Debug.Log("=== Record Class Example ===");

            var recordClass1 = new PlayerStatsRecordClass(100, 50);
            var recordClass2 = recordClass1; // Copy by reference; both variables point to the same instance

            Debug.Log($"[Record Class] Original recordClass1: {recordClass1}\n- This is the original record class instance.");
            Debug.Log($"[Record Class] Copied recordClass2: {recordClass2}\n- This references the same instance as recordClass1.");

            // Modify using 'with' expression
            var recordClass3 = recordClass2 with { AttackPower = 60 };

            Debug.Log($"[Record Class] After modifying recordClass2 using 'with' expression:\nrecordClass1 (unchanged): {recordClass1}\nrecordClass3 (modified): {recordClass3}");

            // Compare record classes
            bool recordClassesAreEqual = recordClass1 == recordClass3;
            Debug.Log($"[Record Class] Are recordClass1 and recordClass3 equal? {recordClassesAreEqual}\n- They are {(recordClassesAreEqual ? "equal" : "not equal")} because their values {(recordClassesAreEqual ? "match" : "differ")}.");

            // Deconstruct record class
            (int recordClassHealth, int recordClassAttackPower) = recordClass1;
            Debug.Log($"[Record Class] Deconstructed recordClass1: Health = {recordClassHealth}, AttackPower = {recordClassAttackPower}");

            // Reference equality
            bool recordClassReferenceEquals = ReferenceEquals(recordClass1, recordClass2);
            Debug.Log($"[Record Class] Reference Equality Checks:\nDo recordClass1 and recordClass2 reference the same instance? {recordClassReferenceEquals}");
            
            bool recordClassReferenceEquals3 = ReferenceEquals(recordClass1, recordClass3);
            Debug.Log($"Do recordClass1 and recordClass3 reference the same instance? {recordClassReferenceEquals3}");

            // ==== Mutable Record Class Example ====
            Debug.Log("=== Mutable Record Class Example ===");

            var mutableRecord1 = new MutablePlayerStatsRecordClass(100, 50);
            var mutableRecord2 = mutableRecord1; // Copy by reference; both variables point to the same instance

            Debug.Log($"[Mutable Record] Original mutableRecord1: {mutableRecord1}\n- This is the original mutable record class instance.");
            Debug.Log($"[Mutable Record] Copied mutableRecord2: {mutableRecord2}\n- This references the same instance as mutableRecord1.");

            // Modify mutableRecord2
            mutableRecord2.AttackPower = 75;

            Debug.Log($"[Mutable Record] After modifying mutableRecord2.AttackPower to 75:\nmutableRecord1: {mutableRecord1}\nmutableRecord2: {mutableRecord2}");

            // Compare mutable records
            bool mutableRecordsAreEqual = mutableRecord1 == mutableRecord2;
            Debug.Log($"[Mutable Record] Are mutableRecord1 and mutableRecord2 equal? {mutableRecordsAreEqual}\n- They are {(mutableRecordsAreEqual ? "equal" : "not equal")} because their values {(mutableRecordsAreEqual ? "match" : "differ")}.");

            // Deconstruct mutable record
            (int mutableRecordHealth, int mutableRecordAttackPower) = mutableRecord1;
            Debug.Log($"[Mutable Record] Deconstructed mutableRecord1: Health = {mutableRecordHealth}, AttackPower = {mutableRecordAttackPower}");

            // Reference equality
            bool mutableRecordReferenceEquals = ReferenceEquals(mutableRecord1, mutableRecord2);
            Debug.Log($"[Mutable Record] Reference Equality Check:\nDo mutableRecord1 and mutableRecord2 reference the same instance? {mutableRecordReferenceEquals}");
        }
    }
}