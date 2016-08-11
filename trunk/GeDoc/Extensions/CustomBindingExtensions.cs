namespace GeDoc
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc;
    using System.Web;

    public static class CustomBindingExtensions
    {
        public static IQueryable<catMarca> ApplyFiltering(this IQueryable<catMarca> data, IList<IFilterDescriptor> filterDescriptors)
        {
            if (filterDescriptors.Any())
            {
                data = data.Where(ExpressionBuilder.Expression<catMarca>(filterDescriptors));
            }
            return data;
        }

        public static IEnumerable ApplyGrouping(this IQueryable<catMarca> data, IList<GroupDescriptor> groupDescriptors)
        {
            Func<IEnumerable<catMarca>, IEnumerable<AggregateFunctionsGroup>> selector = null;
            foreach (var group in groupDescriptors.Reverse())
            {
                if (selector == null)
                {
                    //if (group.Member == "Customer.ContactName")
                    //{
                    //    selector = orders => BuildInnerGroup(orders, o => o.Customer.ContactName);
                    //}
                    //else if (group.Member == "OrderID")
                    //{
                    //    selector = orders => BuildInnerGroup(orders, o => o.OrderID);
                    //}
                    //else if (group.Member == "ShipAddress")
                    //{
                    //    selector = orders => BuildInnerGroup(orders, o => o.ShipAddress);
                    //}
                    //else if (group.Member == "OrderDate")
                    //{
                    //    selector = orders => BuildInnerGroup(orders, o => o.OrderDate);
                    //}
                }
                else
                {
                    //if (group.Member == "Customer.ContactName")
                    //{
                    //    selector = BuildGroup(o => o.Customer.ContactName, selector);
                    //}
                    //else if (group.Member == "OrderID")
                    //{
                    //    selector = BuildGroup(o => o.OrderID, selector);
                    //}
                    //else if (group.Member == "ShipAddress")
                    //{
                    //    selector = BuildGroup(o => o.ShipAddress, selector);
                    //}
                    //else if (group.Member == "OrderDate")
                    //{
                    //    selector = BuildGroup(o => o.OrderDate, selector);
                    //}
                }
            }
            return selector.Invoke(data).ToList();
        }

        private static Func<IEnumerable<catMarca>, IEnumerable<AggregateFunctionsGroup>> BuildGroup<T>(Func<catMarca, T> groupSelector, Func<IEnumerable<catMarca>, IEnumerable<AggregateFunctionsGroup>> selectorBuilder)
        {
            var tempSelector = selectorBuilder;

            return g => g.GroupBy(groupSelector)
                         .Select(c => new AggregateFunctionsGroup
                         {
                             Key = c.Key,
                             HasSubgroups = true,
                             Items = tempSelector.Invoke(c).ToList()
                         });
        }

        private static IEnumerable<AggregateFunctionsGroup> BuildInnerGroup<T>(IEnumerable<catMarca> group, Func<catMarca, T> groupSelector)
        {
            return group.GroupBy(groupSelector)
                    .Select(i => new AggregateFunctionsGroup
                    {
                        Key = i.Key,
                        Items = i.ToList()
                    });
        }

        public static IQueryable<catMarca> ApplyPaging(this IQueryable<catMarca> data, int currentPage, int pageSize)
        {
            if (pageSize > 0 && currentPage > 0)
            {
                data = data.Skip((currentPage - 1) * pageSize);
            }

            data = data.Take(pageSize);
            return data;
        }

        public static IQueryable<catMarca> ApplySorting(this IQueryable<catMarca> data,
            IList<GroupDescriptor> groupDescriptors, IList<SortDescriptor> sortDescriptors)
        {
            if (groupDescriptors.Any())
            {
                foreach (var groupDescriptor in groupDescriptors.Reverse())
                {
                    data = AddSortExpression(data, groupDescriptor.SortDirection, groupDescriptor.Member);
                }
            }

            if (sortDescriptors.Any())
            {
                foreach (SortDescriptor sortDescriptor in sortDescriptors)
                {
                    data = AddSortExpression(data, sortDescriptor.SortDirection, sortDescriptor.Member);
                }
            }
            return data;
        }

        private static IQueryable<catMarca> AddSortExpression(IQueryable<catMarca> data, ListSortDirection sortDirection, string memberName)
        {
            if (sortDirection == ListSortDirection.Ascending)
            {
                switch (memberName)
                {
                    case "marId":
                        data = data.OrderBy(order => order.marId);
                        break;
                    case "marDescripcion":
                        data = data.OrderBy(order => order.marDescripcion);
                        break;
                }
            }
            else
            {
                switch (memberName)
                {
                    case "marId":
                        data = data.OrderByDescending(order => order.marId);
                        break;
                    case "marDescripcion":
                        data = data.OrderByDescending(order => order.marDescripcion);
                        break;
                }
            }
            return data;
        }
    }
}