using LZAI.MgrLib.Model.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LZAIMonitor.Areas.Mgr.Models
{
    public class FuncViewModel
    {
        /// <summary>
        /// 查詢條件
        /// </summary>
        public Filter QueryFilter { get; set; }

        /// <summary>
        /// 查詢結果
        /// </summary>
        public List<MgrFunc> DataList { get; set; }

        public FuncViewModel()
        {
            QueryFilter = new Filter();
        }

        public class Filter
        {
            [DisplayName("功能項目分類")]
            public int QueryFuncTypeId { get; set; }

            [DisplayName("功能名稱")]
            public string QueryFuncName { get; set; }
        }

    }

    /// <summary>
    /// Tree類
    /// </summary>
    public class Node
    {
        public Node() { }
        public Node(int Nodeid, string Text, List<Node> Node, NodeState State = null)
        {
            nodeid = Nodeid;
            text = Text;
            if (Node != null && Node.Count > 0)
                nodes = Node;            

            state = State;
        }
        public int nodeid; //樹的節點Id，區別於資料庫中儲存的資料Id。若要儲存資料庫資料的Id，新增新的Id屬性；若想為節點設定路徑，類中新增Path屬性
        public string text; //節點名稱
        public List<Node> nodes; //子節點，可以用遞迴的方法讀取，方法在下一章會總結

        /// <summary>
        /// public bool checked;
        /// public bool disabled;
        /// public bool expanded;
        /// public bool selected;
        /// </summary>
        public object state;
    }

    public class NodeState
    {
        public bool @checked;
        public bool disabled;
        public bool expanded;
        public bool selected;
    }

}