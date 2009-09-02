using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Trirand.Web.UI.WebControls
{
    public class Searching
    {
        private JQueryGrid _grid;
        private string _searchColunm;
        private string _searchString;
        private string _searchOperation;

        public Searching(JQueryGrid grid, string searchColumn, string searchString, string searchOperation)
        {
            this._grid = grid;
            this._searchColunm = searchColumn;
            this._searchString = searchString;
            this._searchOperation = searchOperation;
        }

        public void PerformSearch(DataView view, string search)
        {
            if (!String.IsNullOrEmpty(search) && Convert.ToBoolean(search))
            {
                // search here                
                JQGridSearchEventArgs args = new JQGridSearchEventArgs()
                {
                    SearchColumn = _searchColunm,
                    SearchString = _searchString,
                    SearchOperation = GetSearchOperationFromString(_searchOperation)
                };
                _grid.OnSearching(args);                

                if (!args.Cancel)
                {
                    view.RowFilter = ConstructFilterExpression(view, args);                            
                }
                _grid.OnSearched(new EventArgs());
            }
        }

        private SearchOperation GetSearchOperationFromString(string searchOperation)
        {
            switch (searchOperation)
            {
                case "eq": return SearchOperation.IsEqualTo;
                case "ne": return SearchOperation.IsNotEqualTo;
                case "lt": return SearchOperation.IsLessThan;
                case "le": return SearchOperation.IsLessOrEqualTo;
                case "gt": return SearchOperation.IsGreaterThan;
                case "ge": return SearchOperation.IsGreaterOrEqualTo;
                case "in": return SearchOperation.IsIn;
                case "ni": return SearchOperation.IsNotIn;
                case "bw": return SearchOperation.BeginsWith;
                case "bn": return SearchOperation.DoesNotEndWith;
                case "ew": return SearchOperation.EndsWith;
                case "en": return SearchOperation.DoesNotEndWith;
                case "cn": return SearchOperation.Contains;
                case "nc": return SearchOperation.DoesNotContain;
                default:
                    throw new Exception("Search operation not known: " + searchOperation);
            }
        }

        private string ConstructFilterExpression(DataView view, JQGridSearchEventArgs args)
        {
            bool needsQuotes = view.ToTable().Columns[args.SearchColumn].DataType == typeof(string);
            string filterExpressionCompare = needsQuotes ? "[{0}] {1} '{2}'" : "[{0}] {1} {2}";
            string filterExpressionIn = "[{0}] {1} ({2})";
            string filterExpressionLike = "[{0}] LIKE '{1}'";
            string filterExpressionNotLike = "[{0}] NOT LIKE '{1}'";

            switch (args.SearchOperation)
            {
                case SearchOperation.IsEqualTo: return String.Format(filterExpressionCompare, args.SearchColumn, "=", args.SearchString);
                case SearchOperation.IsLessOrEqualTo: return String.Format(filterExpressionCompare, args.SearchColumn, "<=", args.SearchString);
                case SearchOperation.IsLessThan: return String.Format(filterExpressionCompare, args.SearchColumn, "<", args.SearchString);
                case SearchOperation.IsGreaterOrEqualTo: return String.Format(filterExpressionCompare, args.SearchColumn, ">=", args.SearchString);
                case SearchOperation.IsGreaterThan: return String.Format(filterExpressionCompare, args.SearchColumn, ">", args.SearchString);
                case SearchOperation.IsIn: return String.Format(filterExpressionIn, args.SearchColumn, "in", args.SearchString);
                case SearchOperation.IsNotIn: return String.Format(filterExpressionIn, args.SearchColumn, "not in", args.SearchString);
                case SearchOperation.BeginsWith: return String.Format(filterExpressionLike, args.SearchColumn, args.SearchString + "%");
                case SearchOperation.Contains: return String.Format(filterExpressionLike, args.SearchColumn, "%" + args.SearchString + "%");
                case SearchOperation.EndsWith: return String.Format(filterExpressionLike, args.SearchColumn, "%" + args.SearchString); 
                case SearchOperation.DoesNotBeginWith: return String.Format(filterExpressionNotLike, args.SearchColumn, args.SearchString + "%");
                case SearchOperation.DoesNotContain: return String.Format(filterExpressionNotLike, args.SearchColumn, "%" + args.SearchString + "%");
                case SearchOperation.DoesNotEndWith: return String.Format(filterExpressionNotLike, args.SearchColumn, "%" + args.SearchString);
            }

            throw new Exception("Invalid search operation.");
        }
        

    }
}
