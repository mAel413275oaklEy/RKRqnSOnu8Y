// 代码生成时间: 2025-10-19 08:48:38
 * a simple interface to switch between them.
 * 
 * Features:
 * - Easy to switch between A and B variants.
 * - Error handling for invalid operations.
 * - Maintainability and scalability in mind.
 */

using System;

namespace ABTestingFramework
{
    public class ABTestFramework<T>
    {
        private T variantA;
        private T variantB;
        private bool isVariantAActive;
        private Random random = new Random();

        // Constructor accepting two variants for the test
        public ABTestFramework(T variantA, T variantB)
        {
            this.variantA = variantA;
            this.variantB = variantB;
            isVariantAActive = true;
        }

        // Method to get the current active variant
        public T GetActiveVariant()
        {
            if (isVariantAActive)
                return variantA;
            else
                return variantB;
        }

        // Method to switch between A and B variants
        public void SwitchVariants()
        {
            isVariantAActive = !isVariantAActive;
        }

        // Method to randomly select and return a variant
        public T GetRandomVariant()
        {
            int result = random.Next(0, 2);
            return result == 0 ? variantA : variantB;
        }

        // Method to set variant A as active
        public void SetVariantAActive()
        {
            if (variantA == null)
                throw new InvalidOperationException("Variant A is not initialized.");
            isVariantAActive = true;
        }

        // Method to set variant B as active
        public void SetVariantBActive()
        {
            if (variantB == null)
                throw new InvalidOperationException("Variant B is not initialized.");
            isVariantAActive = false;
        }
    }
}
