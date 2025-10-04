// 代码生成时间: 2025-10-05 03:42:20
// <summary>
// ChartVisualization.cs 提供了一个Blazor组件，用于在网页上展示可视化图表。
// </summary>
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BlazorChartVisualization
{
    /// <summary>
    /// ChartVisualization组件用于展示图表。
    /// </summary>
    public partial class ChartVisualization
    {
        // 组件参数，用于外部传递数据和配置。
        [Parameter]
        public string ChartType { get; set; } = "line"; // 默认图表类型为折线图
        [Parameter]
        public string Data { get; set; } = ""; // 图表数据，预期为JSON格式

        private ChartJSInterop _chartJsInterop;
        private ElementReference chartContainer;
        private bool isChartInitialized = false;

        /// <summary>
        /// 组件初始化时调用。
        /// </summary>
        protected override void OnInitialized()
        {
            _chartJsInterop = new ChartJSInterop();
        }

        /// <summary>
        /// 在加载组件时，渲染图表。
        /// </summary>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await InitializeChart();
            }
        }

        /// <summary>
        /// 初始化图表，处理数据并调用JSInterop。
        /// </summary>
        private async Task InitializeChart()
        {
            try
            {
                // 假设Data参数是JSON格式的字符串，需要解析成图表所需的格式
                // 这里只是一个示例，具体实现应根据图表库的要求来
                var chartData = ParseChartData(Data);
                await _chartJsInterop.RenderChart(ChartType, chartData, chartContainer);
                isChartInitialized = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing chart: {ex.Message}");
            }
        }

        /// <summary>
        /// 解析图表数据，将JSON字符串转换为图表库所需的数据格式。
        /// </summary>
        private dynamic ParseChartData(string jsonData)
        {
            // 实现JSON解析和数据转换逻辑，这里只是一个示例
            // 具体的解析和转换逻辑应根据图表库的要求来实现
            return jsonData;
        }
    }
}
