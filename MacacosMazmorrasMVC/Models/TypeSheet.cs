namespace MacacosMazmorrasMVC.Models
{
    public class TypeSheet
    {
        private int typeSheetId;
        private string typeSheetClass;

        public int TypeSheetId { get { return typeSheetId; } set { typeSheetId = value; } }
        public string TypeSheetClass { get {  return typeSheetClass; } set { typeSheetClass = value; } }

        public TypeSheet() { }
        public TypeSheet(int typeSheetId, string typeSheetClass)
        {
            TypeSheetId = typeSheetId;
            TypeSheetClass = typeSheetClass;
        }
    }
}
