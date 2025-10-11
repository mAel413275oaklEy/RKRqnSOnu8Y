// 代码生成时间: 2025-10-12 03:01:22
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp
{
    public class VirtualScrollingComponent<T> : ComponentBase
    {
        [Parameter]
        public RenderFragment<T> ChildContent { get; set; }

        [Parameter]
        public int ItemHeight { get; set; } = 50; // Default item height

        [Parameter]
        public int InitialRenderHeight { get; set; } = 300; // Initial render height

        [Parameter]
        public int ItemsPerRow { get; set; } = 1; // Default items per row

        [Parameter]
        public List<T> Items { get; set; } = new List<T>();

        private int _visibleItemsCount;
        private int _scrolledItemCount;
        private double _scrollTop;

        protected override async Task OnInitializedAsync()
        {
            // Calculate the number of visible items based on the initial render height
            _visibleItemsCount = (int)Math.Floor(InitialRenderHeight / ItemHeight) * ItemsPerRow;
            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                // Set the scroll position to the top of the list initially
                _scrollTop = 0;
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private void OnScroll(ScrollEventArgs e)
        {
            _scrollTop = e.Target.ScrollTop;
            _scrolledItemCount = (int)_scrollTop / ItemHeight * ItemsPerRow;
            StateHasChanged(); // Triggers re-rendering of the visible items
        }

        private bool IsVisible(int index)
        {
            // Check if an item is within the visible range
            return index >= _scrolledItemCount && index < _scrolledItemCount + _visibleItemsCount;
        }

        private RenderFragment GetVisibleItems()
        {
            // Return only the items that are visible
            return builder =>
            {
                for (int i = _scrolledItemCount; i < _scrolledItemCount + _visibleItemsCount; i++)
                {
                    if (i < Items.Count && IsVisible(i))
                    {
                        builder.AddContent(0, ChildContent(Items[i]));
                    }
                }
            };
        }

        public RenderFragment VisibleItems => GetVisibleItems();
    }
}
