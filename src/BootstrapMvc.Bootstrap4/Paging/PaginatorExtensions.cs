﻿namespace BootstrapMvc
{
    using System;
    using BootstrapMvc.Core;
    using BootstrapMvc.Paging;

    public static class PaginatorExtensions
    {
        #region Fluent

        public static IItemWriter<T, PaginatorContent> Size<T>(this IItemWriter<T, PaginatorContent> target, PaginatorSize value)
            where T :Paginator
        {
            target.Item.Size = value;
            return target;
        }

        public static IItemWriter<PaginatorGenerator> Autogenerated<T>(this IItemWriter<T, PaginatorContent> target)
            where T : Paginator
        {
            var res = target.Helper.CreateWriter<PaginatorGenerator>(target.Item);
            res.Item.Paginator = target.Item;
            return res;
        }

        public static IItemWriter<PaginatorGenerator> Autogenerated<T>(this IItemWriter<T, PaginatorContent> target, int currentPage, int totalPages)
            where T : Paginator
        {
            return Autogenerated(target).CurrentPage(currentPage).TotalPages(totalPages);
        }

        #endregion

        #region Generating

        public static IItemWriter<Paginator, PaginatorContent> Paginator(this IAnyContentMarker contentHelper)
        {
            return contentHelper.CreateWriter<Paginator, PaginatorContent>();
        }

        public static IItemWriter<Paginator, PaginatorContent> Paginator(this IAnyContentMarker contentHelper, PaginatorSize size)
        {
            return Paginator(contentHelper).Size(size);
        }

        public static PaginatorContent BeginPaginator(this IAnyContentMarker contentHelper)
        {
            return Paginator(contentHelper).BeginContent();
        }

        public static PaginatorContent BeginPaginator(this IAnyContentMarker contentHelper, PaginatorSize size)
        {
            return Paginator(contentHelper).Size(size).BeginContent();
        }

        #endregion
    }
}
