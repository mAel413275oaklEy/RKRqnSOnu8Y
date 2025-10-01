// 代码生成时间: 2025-10-01 16:36:34
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskAssessmentApp
{
    /// <summary>
    /// Risk Assessment component that allows users to input risk factors and assess risks.
    /// </summary>
    public partial class RiskAssessmentSystem
    {
        private readonly List<RiskFactor> riskFactors = new List<RiskFactor>();
        private bool isCalculating;
        private double riskScore;
        private string errorMessage;

        [Parameter]
        public EventCallback OnRiskAssessed { get; set; }

        /// <summary>
        /// Adds a new risk factor to the list.
        /// </summary>
        /// <param name="factor">The risk factor to add.</param>
        public void AddRiskFactor(RiskFactor factor)
        {
            if (factor == null)
            {
                throw new ArgumentNullException(nameof(factor));
            }

            riskFactors.Add(factor);
        }

        /// <summary>
        /// Removes a risk factor from the list.
        /// </summary>
        /// <param name="factor">The risk factor to remove.</param>
        public void RemoveRiskFactor(RiskFactor factor)
        {
            riskFactors.Remove(factor);
        }

        /// <summary>
        /// Initiates the risk assessment process.
        /// </summary>
        private async Task AssessRiskAsync()
        {
            if (isCalculating)
            {
                errorMessage = "Risk assessment is already in progress.";
                StateHasChanged();
                return;
            }

            isCalculating = true;
            errorMessage = null;
            riskScore = 0;

            try
            {
                foreach (var factor in riskFactors)
                {
                    riskScore += factor.Influence;
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"An error occurred: {ex.Message}";
            }
            finally
            {
                isCalculating = false;
                await OnRiskAssessed.InvokeAsync(new RiskAssessmentEventArgs { Score = riskScore, ErrorMessage = errorMessage });
            }
        }
    }

    /// <summary>
    /// Represents a risk factor with an influence value.
    /// </summary>
    public class RiskFactor
    {
        public string Name { get; set; }
        public double Influence { get; set; }
    }

    /// <summary>
    /// Event arguments for risk assessment completion.
    /// </summary>
    public class RiskAssessmentEventArgs : EventArgs
    {
        public double Score { get; set; }
        public string ErrorMessage { get; set; }
    }
}
