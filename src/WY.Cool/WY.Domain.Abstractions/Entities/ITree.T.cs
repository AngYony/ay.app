﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WY.Domain.Abstractions.Entities
{
    /// <summary>
    /// 树形数据接口
    /// </summary>
    /// <typeparam name="T">节点数据类型</typeparam>
    public interface ITree<T>
    {
        /// <summary>
        /// 父节点
        /// </summary>
        T? Parent { get; set; }

        /// <summary>
        /// 子节点集合
        /// </summary>
        IList<T> Children { get; set; }

        /// <summary>
        /// 节点深度，根的深度为0
        /// </summary>
        int Depth { get; }

        /// <summary>
        /// 是否是根节点
        /// </summary>
        bool IsRoot { get; }

        /// <summary>
        /// 是否是叶节点
        /// </summary>
        bool IsLeaf { get; }

        /// <summary>
        /// 是否有子节点
        /// </summary>
        bool HasChildren { get; }

        /// <summary>
        /// 节点路径（UNIX路径格式，以“/”分隔）
        /// </summary>
        string? Path { get; }
    }
}
