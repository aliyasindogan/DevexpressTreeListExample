using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevexpressTreeListExample.Models;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DevexpressTreeListExample
{
    public partial class Form1 : Form
    {
        private int fontSizeDeltaControl = 0;

        public Form1()
        {
            InitializeComponent();
            treeListFill();
            LookUpEditFill();
        }

        #region Crud Operations

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(lookUpEditUserType.EditValue) > 0)
                {
                    #region Önce Kulanıcı Tipi Silme işlemi

                    using (DatabaseContext context = new DatabaseContext())
                    {
                        int _selectedId = Convert.ToInt32(lookUpEditUserType.EditValue);
                        var dataRemoveList = context.CategoryUserTypes.Where(x => x.UserTypeID == _selectedId).ToList();
                        foreach (var item in dataRemoveList.ToList())
                        {
                            var list = context.CategoryUserTypes.FirstOrDefault(x => x.Id == item.Id);
                            context.CategoryUserTypes.Remove(list);
                            context.SaveChanges();
                        }
                    }

                    #endregion Önce Kulanıcı Tipi Silme işlemi

                    #region Kaydetme İşlemi

                    using (DatabaseContext db = new DatabaseContext())
                    {
                        foreach (TreeListNode item in treeList.Nodes)
                        {
                            string name = item.GetDisplayText("CategoryName");
                            Category _category = db.Categories.FirstOrDefault(x => x.CategoryName == name);
                            if (_category.CategoryName == item.GetDisplayText("CategoryName"))
                            {
                                if (item.CheckState == CheckState.Indeterminate)
                                {
                                    db.CategoryUserTypes.Add(new CategoryUserType
                                    {
                                        CategoryID = _category.Id,
                                        UserTypeID = Convert.ToInt32(lookUpEditUserType.EditValue),
                                        IsChecked = true,
                                    });
                                    db.SaveChanges();

                                    if (item.HasChildren)
                                        GetChildNode(item, _category);
                                }
                                else
                                {
                                    if ((bool)item.GetValue("Checked"))
                                    {
                                        db.CategoryUserTypes.Add(new CategoryUserType
                                        {
                                            CategoryID = _category.Id,
                                            UserTypeID = Convert.ToInt32(lookUpEditUserType.EditValue),
                                            IsChecked = true,
                                        });
                                        db.SaveChanges();

                                        if (item.HasChildren)
                                            GetChildNode(item, _category);
                                    }
                                    else
                                    {
                                        db.CategoryUserTypes.Add(new CategoryUserType
                                        {
                                            CategoryID = _category.Id,
                                            UserTypeID = Convert.ToInt32(lookUpEditUserType.EditValue),
                                            IsChecked = false,
                                        });
                                        db.SaveChanges();
                                        if (item.HasChildren)
                                            GetChildNode(item, _category);
                                    }
                                }
                            }
                        }
                    }

                    #endregion Kaydetme İşlemi

                    MessageBox.Show("Kategoriler Başarılı Bir Şekilde Eklendi !");
                }
                else
                {
                    MessageBox.Show("Lütfen Kullanıcı Tipi Seçiniz !");
                }

                treeList1Fill(Convert.ToInt32(lookUpEditUserType.EditValue));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata Oluştu ! \nHata Mesajı: " + ex.ToString());
            }
        }

        private void GetChildNode(TreeListNode ChildNode, Category category)
        {
            //Alt Kategorileri kayıt ediyor.
            using (DatabaseContext db = new DatabaseContext())
            {
                foreach (TreeListNode item2 in ChildNode.Nodes)
                {
                    string name = item2.GetDisplayText("CategoryName");
                    Category _category = db.Categories.FirstOrDefault(x => x.CategoryName == name);
                    if (item2.CheckState == CheckState.Indeterminate)
                    {
                        db.CategoryUserTypes.Add(new CategoryUserType
                        {
                            CategoryID = _category.Id,
                            UserTypeID = Convert.ToInt32(lookUpEditUserType.EditValue),
                            IsChecked = true,
                        });
                        db.SaveChanges();

                        if (item2.HasChildren)
                            GetChildNode(item2, _category);
                    }
                    else
                    {
                        if ((bool)item2.GetValue("Checked"))
                        {
                            db.CategoryUserTypes.Add(new CategoryUserType
                            {
                                CategoryID = _category.Id,
                                SubCategoryID = (int)_category.SubCategoryID,
                                UserTypeID = Convert.ToInt32(lookUpEditUserType.EditValue),
                                IsChecked = true,
                            });
                            db.SaveChanges();
                        }
                        else
                        {
                            db.CategoryUserTypes.Add(new CategoryUserType
                            {
                                CategoryID = _category.Id,
                                SubCategoryID = (int)_category.SubCategoryID,
                                UserTypeID = Convert.ToInt32(lookUpEditUserType.EditValue),
                                IsChecked = false,
                            });
                            db.SaveChanges();
                        }
                        if (item2.HasChildren)
                            GetChildNode(item2, _category);
                    }
                }
            }
        }

        private void btnListRefresh_Click(object sender, EventArgs e)
        {
            treeListFill();
            lookUpEditUserType.EditValue = null;
        }

        private void lookUpEditUserType_EditValueChanged(object sender, EventArgs e)
        {
            treeList1Fill(Convert.ToInt32(lookUpEditUserType.EditValue));
            if (Convert.ToInt32(lookUpEditUserType.EditValue) > 0)
            {
                groupControl3.Text = "Kayıtlı Kategori Listesi (Tree List)" + " / " + lookUpEditUserType.Text;
            }
            else
            {
                groupControl3.Text = "Kayıtlı Kategori Listesi (Tree List)";
            }
        }

        private void OnBeforeFocusNode(object sender, BeforeFocusNodeEventArgs e)
        {
            e.CanFocus = false;
        }

        private void OnNodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            //Ana Kategorileri Bold olarak fontu düzenliyor.
            if (e.Node.Level == 0)
            {
                if (fontSizeDeltaControl == 0)
                {
                    e.Appearance.FontSizeDelta += 1;
                    fontSizeDeltaControl++;
                }
                e.Appearance.FontStyleDelta = FontStyle.Bold;
            }
            if (e.Node.Level == 1 && e.Node.Nodes.Count > 0)
                e.Appearance.FontStyleDelta = FontStyle.Bold;
        }

        #endregion Crud Operations

        #region Methods

        private void LookUpEditFill()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var comboboxUserTypeList = db.UserTypes.Select(x => new ComboboxViewModel()
                {
                    Id = x.Id.ToString(),
                    Name = x.UserTypeName,
                }).ToList();

                lookUpEditUserType.Properties.ValueMember = "Id";
                lookUpEditUserType.Properties.DisplayMember = "Name";
                lookUpEditUserType.Properties.NullText = "Lütfen Seçim Yapınız";
                lookUpEditUserType.Properties.DataSource = comboboxUserTypeList.ToList();
            }
        }

        private void treeListFill()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                treeList.Appearance.Row.BackColor = Color.Transparent;
                treeList.Appearance.Empty.BackColor = Color.Transparent;
                treeList.BackColor = Color.Transparent;
                treeList.KeyFieldName = "Id";
                treeList.ParentFieldName = "SubCategoryID";
                treeList.CheckBoxFieldName = "Checked";
                treeList.TreeViewFieldName = "CategoryName";
                treeList.OptionsView.FocusRectStyle = DrawFocusRectStyle.None;
                treeList.OptionsBehavior.Editable = false;
                treeList.OptionsBehavior.ReadOnly = true;
                treeList.OptionsBehavior.AllowRecursiveNodeChecking = true;
                treeList.NodeCellStyle += OnNodeCellStyle;
                treeList.BeforeFocusNode += OnBeforeFocusNode;
                treeList.DataSource = db.Categories.Select(x => new CategoryViewModel
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName,
                    SubCategoryID = x.SubCategoryID,
                    Checked = true,
                }).ToList();
                treeList.ForceInitialize();

                treeList.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
                //Tüm kategoriye checkbox ekliyor.
                treeList.OptionsView.CheckBoxStyle = DefaultNodeCheckBoxStyle.Check;

                #region Manuel Chechbox Created

                //Nodes[1]
                // treeList.Nodes[1].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;

                //treeList.Nodes[1].Nodes[0]
                // treeList.Nodes[1].Nodes[0].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;

                //treeList.Nodes[1].Nodes[1]
                // treeList.Nodes[1].Nodes[1].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;

                //treeList.Nodes[1].Nodes[1].Nodes[0]
                //treeList.Nodes[1].Nodes[1].Nodes[0].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;

                //treeList.Nodes[1].Nodes[1].Nodes[1]
                //treeList.Nodes[1].Nodes[1].Nodes[1].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;

                //treeList.Nodes[2]
                //  treeList.Nodes[2].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;

                //treeList.Nodes[2].Nodes[0]
                //  treeList.Nodes[2].Nodes[0].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;

                //treeList.Nodes[2].Nodes[1]
                //  treeList.Nodes[2].Nodes[1].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;

                //treeList.Nodes[2].Nodes[2]
                //treeList.Nodes[2].Nodes[2].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;

                #endregion Manuel Chechbox Created

                treeList.ExpandAll();
            }
        }

        private void treeList1Fill(int id)
        {
            try
            {
                using (DatabaseContext db = new DatabaseContext())
                {
                    treeList1.Appearance.Row.BackColor = Color.Transparent;
                    treeList1.Appearance.Empty.BackColor = Color.Transparent;
                    treeList1.BackColor = Color.Transparent;
                    treeList1.KeyFieldName = "Id";
                    treeList1.ParentFieldName = "SubCategoryID";
                    treeList1.TreeViewFieldName = "CategoryName";
                    treeList1.RootValue = "0";
                    treeList1.OptionsView.FocusRectStyle = DrawFocusRectStyle.None;
                    treeList1.OptionsBehavior.Editable = false;
                    treeList1.OptionsBehavior.ReadOnly = true;
                    treeList1.OptionsBehavior.AllowRecursiveNodeChecking = true;
                    treeList1.NodeCellStyle += OnNodeCellStyle;
                    treeList1.BeforeFocusNode += OnBeforeFocusNode;
                    var result = (from categoryUserTypes in db.CategoryUserTypes
                                  join category in db.Categories on categoryUserTypes.CategoryID equals category.Id
                                  join userType in db.UserTypes on categoryUserTypes.UserTypeID equals userType.Id
                                  where categoryUserTypes.UserTypeID == id && categoryUserTypes.IsChecked == true
                                  select new CategoryViewModel
                                  {
                                      Id = categoryUserTypes.CategoryID,
                                      CategoryName = category.CategoryName,
                                      SubCategoryID = categoryUserTypes.SubCategoryID,
                                  }).ToList();
                    treeList1.DataSource = result;
                    treeList1.ForceInitialize();
                    treeList1.ExpandAll();
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion Methods
    }
}