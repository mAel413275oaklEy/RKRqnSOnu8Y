// 代码生成时间: 2025-10-26 08:55:53
using Microsoft.AspNetCore.Components;

namespace YourApp.Components
{
    // 标签页模型
    public class TabItem
    {
        public string Title { get; set; }
        public RenderFragment ChildContent { get; set; }
    }
# FIXME: 处理边界情况

    // 标签页切换器组件
    public class TabbedComponent<T> : ComponentBase
# NOTE: 重要实现细节
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        private List<T> tabs = new List<T>();
        private T selectedTab;

        // 添加标签页
# FIXME: 处理边界情况
        public void AddTab(T tabItem)
# 优化算法效率
        {
            if (tabItem == null)
            {
                throw new ArgumentNullException(nameof(tabItem), "Tab item cannot be null.");
            }

            tabs.Add(tabItem);
            if (tabs.Count == 1)
            {
                // 默认选中第一个标签页
                SelectTab(tabItem);
            }
        }

        // 选择标签页
        private void SelectTab(T tabItem)
        {
            if (tabs.Contains(tabItem))
            {
                selectedTab = tabItem;
            }
            else
            {
                throw new ArgumentException("Selected tab is not part of the tab list.", nameof(tabItem));
# 改进用户体验
            }
        }

        // 渲染当前选中的标签页内容
# FIXME: 处理边界情况
        private RenderFragment GetCurrentTabContent()
        {
            return builder =>
            {
                foreach (var tab in tabs)
                {
# 优化算法效率
                    if (tab.Equals(selectedTab))
                    {
                        builder.AddContent(0, tab.Content);
                        break;
                    }
                }
# 添加错误处理
            };
        }
    }
}

/*
 * 使用方法：
 * <TabbedComponent>
 *     <TabItem Title="Tab 1">
# 改进用户体验
 *         <div>Content of Tab 1</div>
 *     </TabItem>
 *     <TabItem Title="Tab 2">
 *         <div>Content of Tab 2</div>
# 改进用户体验
 *     </TabItem>
 * </TabbedComponent>
 */