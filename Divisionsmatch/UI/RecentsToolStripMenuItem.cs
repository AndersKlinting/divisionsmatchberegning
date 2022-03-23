//Author: Tony Brix
//Website: tonybrix.info
//
//
//
using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Win32;
using System.ComponentModel;

/// <summary>
/// This is a menu item that reads and writes to the registry to store a list of recently opened documents.
/// </summary>
[Description("This is a menu item that reads and writes to the registry to store a list of recently opened documents.")]
[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
public class RecentsToolStripMenuItem : ToolStripMenuItem
{

    private int varMaxItems = 5;
    private RegistryKey varRegistryKey = Registry.CurrentUser.CreateSubKey("Software\\" + Application.CompanyName + "\\" + Application.ProductName + "\\Recent");
    /// <summary>
    /// Constructor of the menu item
    /// </summary>
    public RecentsToolStripMenuItem()
    {
        this.Enabled = false;
    }

    /// <summary>
    /// The registry key where the file names are saved.
    /// </summary>
    /// <example>
    /// <code>
    /// recentToolStripMenuItem.RegistryKey = Registry.CurrentUser.CreateSubKey("Software\\" + Application.CompanyName + "\\" + Application.ProductName + "\\Recent");
    /// </code>
    /// </example>
    /// <value>
    /// "Registry.CurrentUser.CreateSubKey("Software\\" + Application.CompanyName + "\\" + Application.ProductName + "\\Recent");"
    /// </value>
    [Category("Data")]
    [Description("The registry key where the file names are saved.")]
    [DefaultValue("\"Registry.CurrentUser.CreateSubKey(\"Software\\\" + Application.CompanyName \"\\\" + Application.ProductName + \"\\Recent\")\"")]
    public RegistryKey Key
    {
        get { return varRegistryKey; }
        set { varRegistryKey = value; }
    }

    /// <summary>
    /// This is the function that is executed when a file name is selected from the list.
    /// </summary>
    /// <remarks>
    /// ((ToolStripDropDownItem)sender).Text can be used to find the value of the one that was clicked.
    /// </remarks>
    /// <example>
    /// <code>
    /// recentToolStripMenuItem.ItemClick += new System.EventHandler(this.recentItem_Click);
    /// </code>
    /// </example>
    [Category("Action")]
    [Description("This is the function that is executed when a file name is selected from the list. ((ToolStripDropDownItem)sender).Text can be used to find the value of the one that was clicked.")]
    public event EventHandler ItemClick;
    
    /// <summary>
    /// Maximum number of items in the list.
    /// </summary>
    /// <value>5</value>
    [Category("Layout")]
    [Description("Maximum number of items in the list.")]
    [DefaultValue(5)]
    public int MaxItems
    {
        get { return varMaxItems; }
        set { varMaxItems = value; }
    }
    
    /// <summary>
    /// Update the recent list with the values in the registry.
    /// </summary>
    /// <remarks>
    /// Use after initializing.
    /// </remarks>
    public void UpdateList()
    {
        this.DropDownItems.Clear();
        if (varRegistryKey == null || varRegistryKey.ValueCount == 0)
        {
            this.Enabled = false;
        }
        else
        {
            this.Enabled = true;
            int i = 0;
            foreach (string name in varRegistryKey.GetValueNames())
            {
                i++;
                string value = (string)varRegistryKey.GetValue(name);
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Name = name;
                item.Size = new System.Drawing.Size(156, 22);
                item.Tag = value;
                item.Text = "&" + i.ToString() + " " + value;
                if (this.ItemClick == null)
                {
                    item.Enabled = false;
                }
                else
                {
                    item.Click += this.ItemClick;
                }
                this.DropDownItems.Add(item);
            }

            this.DropDown.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Delete)
                {
                    foreach (ToolStripMenuItem item in ((ToolStrip)s).Items)
                    {
                        if (item.Selected)
                        {
                            RemoveRecentItem((string)item.Tag);
                            break;
                        }
                    }
                }
            };
        }
    }
    /// <summary>
    /// Add a file path to the recents list.
    /// </summary>
    /// <param name="filePath">
    /// The path to the file.
    /// </param>
    /// <example>
    /// <code>
    /// recentToolStripMenuItem.AddRecentItem(openFileDialog.FileName);
    /// </code>
    /// </example>
    public void AddRecentItem(string filePath)
    {
        try
        {
            int index = varRegistryKey.ValueCount;
            foreach (string name in varRegistryKey.GetValueNames())
            {
                if (((string)varRegistryKey.GetValue(name)) == filePath)
                {
                    index = int.Parse(name) - 1;
                    break;
                }
            }
            for (int i = index; i > 0; i--)
            {
                if (i < varMaxItems)
                {
                    varRegistryKey.SetValue((i + 1).ToString(), varRegistryKey.GetValue(i.ToString()), RegistryValueKind.String);
                }
            }
        }
        catch
        {
            foreach (string name in varRegistryKey.GetValueNames())
            {
                varRegistryKey.DeleteValue(name);
            }
        }
        varRegistryKey.SetValue("1", filePath, RegistryValueKind.String);
        UpdateList();
    }

    /// <summary>
    /// Remove a file path to the recents list.
    /// </summary>
    /// <param name="filePath">
    /// The path to the file.
    /// </param>
    /// <example>
    /// <code>
    /// recentToolStripMenuItem.RemoveRecentItem(openFileDialog.FileName);
    /// </code>
    /// </example>
    public void RemoveRecentItem(string filePath)
    {
        try
        {
            int cnt = varRegistryKey.ValueCount;
            int index = -1;
            foreach (string name in varRegistryKey.GetValueNames())
            {
                if (((string)varRegistryKey.GetValue(name)) == filePath)
                {
                    index = int.Parse(name);
                    break;
                }
            }

            if (index > 0)
            {
                for (int i = index; i < cnt; i++)
                {
                    varRegistryKey.SetValue(i.ToString(), varRegistryKey.GetValue((i + 1).ToString()), RegistryValueKind.String);
                }
                varRegistryKey.DeleteValue(cnt.ToString());
            }
        }
        catch
        {
        }

        UpdateList();
    }
}
