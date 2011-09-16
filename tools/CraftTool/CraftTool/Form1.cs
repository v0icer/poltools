﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ConfigUtil;
using POLTools.ConfigRepository;
using POLTools.Itemdesc;
using POLTools.Package;
using CraftTool.Forms;

namespace CraftTool
{
	public partial class Form1 : Form
	{
		private bool _data_loaded = false;

		#region Generic Form Stuff & Reusable Functions
		public Form1()
		{
			InitializeComponent();

			foreach (TabPage tab in TabControl1.TabPages)
			{
				if (tab != tabPage1)
					tab.Enabled = false;
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			Settings.Global.LoadSettings();
		}

		private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Forms.SettingsForm.SettingsForm settings_form = new Forms.SettingsForm.SettingsForm();
			settings_form.ShowDialog(this);
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Forms.AboutForm.AboutForm about_form = new CraftTool.Forms.AboutForm.AboutForm();
			about_form.ShowDialog(this);
		}

		private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			TabPage selected_tab = TabControl1.SelectedTab;
			if (!_data_loaded && selected_tab != tabPage1)
			{
				TabControl1.SelectedTab.Enabled = false;
				foreach (Control cntrl in selected_tab.Controls)
				{
					cntrl.Enabled = false;
				}
			}
			else
			{
				foreach (Control cntrl in selected_tab.Controls)
				{
					cntrl.Enabled = true;
				}

				if (!selected_tab.Enabled)
				{
					if (selected_tab == tabPage2)
						PopulateItemDesc();
					if (selected_tab == tabPage3)
						PopulateMaterials();
					if (selected_tab == tabPage4)
						PopulateToolOnMaterial();
				}

				selected_tab.Enabled = true;
			}
		}

		private void RemoveConfigTreeNode(TreeView treeview, string cfgname)
		{
			TreeNode selected = treeview.SelectedNode;
			if (selected == null)
			{
				MessageBox.Show("You need to select a tree node.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			TreeNode parent = selected;
			while (parent.Parent != null)
			{
				parent = parent.Parent;
			}

			POLPackage package = PackageCache.GetPackage(parent.Name);
			ConfigFile config_file;
			string config_path = package.GetPackagedConfigPath(cfgname);
			if (config_path == null) // Its a pseudo config & elem at this point then. (Not on disk)
				config_path = package.path + @"\config\"+cfgname;
			config_file = ConfigRepository.global.LoadConfigFile(config_path);

			if (parent == selected)
			{
				foreach (TreeNode child in selected.Nodes)
				{
					if (child == null)
						continue;
					config_file.RemoveConfigElement(child.Name);
				}
			}
			else
			{
				config_file.RemoveConfigElement(selected.Name);
			}

			TreeView tvparent = selected.TreeView;

			toolonmaterial_treeview.Nodes.Remove(selected);

			if (parent == selected || tvparent.VisibleCount < 1)
				ConfigRepository.global.UnloadConfigFile(config_path);
		}

		private void CreateConfigFileForPackage(string cfgname)
		{
			Forms.SelectionPicker.SelectionPicker picker = new Forms.SelectionPicker.SelectionPicker("Select a package", PackageCache.Global.packagenames);
			picker.ShowDialog(this);
			if (picker.DialogResult != DialogResult.OK)
				return;
			else if (PackageCache.GetPackage(picker.text) == null)
			{
				MessageBox.Show("Invalid package name '" + picker.text + "'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			materials_tree_view.Nodes.Add(picker.text, ":" + picker.text + ":"+cfgname);

			POLPackage pkg = PackageCache.GetPackage(picker.text);
			string filepath = pkg.path + @"\config\"+cfgname;
			ConfigFile config_file = new ConfigFile(filepath);
			ConfigRepository.global.AddConfigFile(config_file);
		}

		private void AddConfigElemForTreeNode(TreeNode selected, string cfgname, string elemprefix, string elemname)
		{
			string package_name = POLPackage.ParsePackageName(selected.Text);
			POLPackage package = PackageCache.GetPackage(package_name);
			ConfigFile config_file;
			string config_path = package.GetPackagedConfigPath(cfgname);
			if (config_path == null) // Its a pseudo config & elem at this point then. (Not on disk)
				config_path = package.path + @"\config\"+cfgname;

			config_file = ConfigRepository.global.LoadConfigFile(config_path);
			ConfigElem elem = new ConfigElem(elemprefix, elemname);
			config_file.AddConfigElement(elem);

			selected.Nodes.Add(elemname, elemname);
		}

		private TreeNode GetParentTreeNode(TreeNode thenode)
		{
			TreeNode nodeparent = thenode;
			while (nodeparent.Parent != null)
			{
				nodeparent = nodeparent.Parent;
			}
			return nodeparent;
		}

		private TreeNode CheckForSelectedNode(TreeView treeview)
		{
			TreeNode selected = treeview.SelectedNode;
			if (selected == null)
			{
				MessageBox.Show("You need to select a tree node.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return null;
			}
			return selected;
		}

		#endregion

		private void button1_Click(object sender, EventArgs e)
		{
			TB_loadoutput.Clear();
			BTN_load_info.Enabled = false;
			if (!Directory.Exists(Settings.Global.rootdir))
			{
				TB_loadoutput.AppendText("Invalid root directory. Please check settings."+Environment.NewLine+Settings.Global.rootdir);
				return;
			}
			
			TB_loadoutput.AppendText("Checking for packages... ");
			PackageCache.LoadPackages(Settings.Global.rootdir);

			List<POLTools.Package.POLPackage> packages = PackageCache.Global.packagelist;
			TB_loadoutput.AppendText("Enabled pkg.cfg files found = " + packages.Count + Environment.NewLine);

			string[] file_names = { "itemdesc.cfg", "materials.cfg", "toolonmaterial.cfg", "craftmenus.cfg", "craftitems.cfg" };
			foreach (POLTools.Package.POLPackage package in packages)
			{
				TB_loadoutput.AppendText(package.name+Environment.NewLine);

				foreach ( string filename in file_names )
				{
					string config_path = package.GetPackagedConfigPath(filename);
					if (config_path != null)
					{
						ConfigFile config_file = ConfigRepository.global.LoadConfigFile(config_path);
						
						TB_loadoutput.AppendText("  Loaded " + config_file.filename + Environment.NewLine);
					}
				}
			}

			POLTools.Itemdesc.ItemdescCache.Global.LoadItemdescFiles();

			_data_loaded = true;
		}
		
		#region Itemdesc Tab Stuff
		private void PopulateItemDesc()
		{
			List<ConfigElem> config_elems = ItemdescCache.Global.GetAllObjTypeElems();
			foreach (ConfigElem config_elem in config_elems)
			{
				string item_name = string.Empty;
				if (config_elem.PropertyExists("name"))
				{
					int count = 0;
					List<string> name_entries = config_elem.GetConfigStringList("name");
					foreach (string name in name_entries)
					{
						if (count > 0)
							item_name += ", ";
						item_name += name;
						count++;
					}
				}
				Object[] row = new Object[3];
				row[0] = global::CraftTool.Properties.Resources.unused;
				row[1] = config_elem.name;
				row[2] = item_name;
				itemdesc_datagrid.Rows.Add(row);
			}
			itemdesc_datagrid.ScrollBars = ScrollBars.None;
			itemdesc_datagrid.Refresh();
			itemdesc_datagrid.ScrollBars = ScrollBars.Vertical;
			itemdesc_datagrid.ClearSelection();
			itemdesc_datagrid.Refresh();
		}


		private void itemdesc_datagrid_RowEnter(object sender, DataGridViewCellEventArgs e)
		{
			TB_itemdescinfo.Clear();

			DataGridViewRow row = itemdesc_datagrid.Rows[e.RowIndex];
			//ConfigElem itemdesc_elem = ConfigRepository.global.GetElemFromConfigFiles("itemdesc.cfg", row.Cells[1].Value.ToString());
			ConfigElem itemdesc_elem = ItemdescCache.Global.GetElemForObjType(row.Cells[1].Value.ToString());
			foreach (string propname in itemdesc_elem.ListConfigElemProperties())
			{
				foreach (string value in itemdesc_elem.GetConfigStringList(propname))
				{
					TB_itemdescinfo.AppendText(propname + "	" + value + Environment.NewLine);
				}
			}

			label4.Text = itemdesc_elem.configfile.fullpath;
			itemdesc_picture.Image = (Bitmap)row.Cells[0].Value;
		}

		private void copyObjTypeToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DataGridViewRow row = itemdesc_datagrid.CurrentRow;
			DataGridViewCell cell = row.Cells[1];
			Clipboard.SetText(cell.Value.ToString());
		}
		#endregion

		#region Materials Tab Stuff
		public void PopulateMaterials()
		{
			foreach (POLTools.Package.POLPackage package in PackageCache.Global.packagelist)
			{
				string materials_cfg_path = package.GetPackagedConfigPath("materials.cfg");
				if (materials_cfg_path == null)
					continue;

				TreeNode pkg_node = materials_tree_view.Nodes.Add(package.name, ":"+package.name+":materials.cfg");
				ConfigFile materials_config = ConfigRepository.global.LoadConfigFile(materials_cfg_path);
				foreach (ConfigElem cfg_elem in materials_config.GetConfigElemRefs())
				{
					AddMaterialsObjType(pkg_node, cfg_elem.name);
				}
			}
		}

		private TreeNode AddMaterialsObjType(TreeNode parent_node, string objtype)
		{
			string nodename = objtype;
			ConfigElem itemdesc_elem = ItemdescCache.Global.GetElemForObjType(objtype);
			if (itemdesc_elem != null)
			{
				if (itemdesc_elem.PropertyExists("Name"))
					nodename += "   [" + itemdesc_elem.GetConfigString("Name") + "]";
			}

			TreeNode added = parent_node.Nodes.Add(objtype, nodename);
			return added;
		}

		private void materials_tree_view_AfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeNode selected = materials_tree_view.SelectedNode;
			
			materials_textbox.Clear();
			combobox_materials_changeto.Items.Clear();
			combobox_materials_changeto.Text = "";
			foreach (Control control in groupBox7.Controls)
			{
				if (control is TextBox)
					((TextBox)control).Clear();
			}

			if (selected.Parent == null)
				return;
						
			ConfigElem materials_elem = ConfigRepository.global.FindElemInConfigFiles("materials.cfg", selected.Name);
			label5.Text = materials_elem.configfile.fullpath;
			combobox_materials_changeto.Items.Clear();
			foreach (string propname in materials_elem.ListConfigElemProperties())
			{
				foreach (string value in materials_elem.GetConfigStringList(propname))
				{
					materials_textbox.AppendText(propname + "	" + value + Environment.NewLine);
				}
			}
			materials_picture.Image = global::CraftTool.Properties.Resources.unused;
			if (materials_elem.PropertyExists("Category"))
				TB_materials_category.Text = materials_elem.GetConfigString("Category");
			if (materials_elem.PropertyExists("Color"))
				TB_materials_color.Text = materials_elem.GetConfigString("Color");
			if (materials_elem.PropertyExists("Difficulty"))
				TB_materials_difficulty.Text = materials_elem.GetConfigString("Difficulty");
			if (materials_elem.PropertyExists("Quality"))
				TB_materials_quality.Text = materials_elem.GetConfigString("Quality");
			if (materials_elem.PropertyExists("CreatedScript"))
				TB_materials_createdscript.Text = materials_elem.GetConfigString("CreatedScript");

			List<string> itemdesc_names = ItemdescCache.Global.GetAllObjTypeNames();
			combobox_materials_changeto.Items.AddRange(itemdesc_names.ToArray());
			if (materials_elem.PropertyExists("ChangeTo"))
			{
				string changeto = materials_elem.GetConfigString("ChangeTo").ToLower();
				//int pos = itemdesc_names.IndexOf(materials_elem.GetConfigString("ChangeTo"));
				int pos = combobox_materials_changeto.FindString(changeto);
				combobox_materials_changeto.SelectedIndex = pos; // Account for 'None'
			}
		}

		private void BTN_materials_update_Click(object sender, EventArgs e)
		{
			TreeNode selected = CheckForSelectedNode(materials_tree_view);
			if (selected == null)
				return;
			TreeNode nodeparent = GetParentTreeNode(selected);
					
			POLPackage package = PackageCache.GetPackage(nodeparent.Name);
			ConfigFile materials_cfg;
			string config_path = package.GetPackagedConfigPath("materials.cfg");
			materials_cfg = ConfigRepository.global.LoadConfigFile(config_path);
			ConfigElem original = materials_cfg.GetConfigElem(selected.Name);

			ConfigElem newelem = new ConfigElem(original.type, original.name);
			newelem.AddConfigLine("Category", TB_materials_category.Text);
			newelem.AddConfigLine("Color", TB_materials_color.Text);
			newelem.AddConfigLine("Difficulty", TB_materials_difficulty.Text);
			newelem.AddConfigLine("Quality", TB_materials_quality.Text);
			newelem.AddConfigLine("ChangeTo", combobox_materials_changeto.Text);
			newelem.AddConfigLine("CreatedScript", TB_materials_createdscript.Text);
			
			materials_cfg.RemoveConfigElement(selected.Name);
			materials_cfg.AddConfigElement(newelem);

			materials_tree_view_AfterSelect(sender, null);
		}

		private void materials_context_strip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			return;
		}

		private void createNewConfigToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CreateConfigFileForPackage("materials.cfg");
		}

		private void addNewElementToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode selected = CheckForSelectedNode(materials_tree_view);
			if (selected == null)
				return;
			TreeNode nodeparent = GetParentTreeNode(selected);
			
			Forms.SelectionPicker.SelectionPicker picker = new Forms.SelectionPicker.SelectionPicker("Select a material", ItemdescCache.Global.GetAllObjTypes());
			picker.ShowDialog(this);
			if (picker.result != DialogResult.OK)
				return;
			else
			{
				if (ItemdescCache.Global.GetElemForObjType(picker.text) == null)
				{
					MessageBox.Show("Invalid object type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}

			AddConfigElemForTreeNode(nodeparent, "materials.cfg", "Material", picker.text);
		}
		
		private void removeElementToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RemoveConfigTreeNode(materials_tree_view, "materials.cfg");
		}

		private void BTN_materials_write_Click(object sender, EventArgs e)
		{
			// First figure out if any existing configs need to be deleted.
			foreach (POLPackage package in PackageCache.Global.packagelist)
			{
				var materials_cfg_path = package.GetPackagedConfigPath("materials.cfg");
				if (materials_cfg_path == null)
					continue;
				bool loaded = ConfigRepository.global.IsPathCached(materials_cfg_path);
				if (!loaded) // Delete
					File.Delete(materials_cfg_path);
			}

			// Write the tree information to the config files.
			foreach (TreeNode node in materials_tree_view.Nodes)
			{
				if (node.Parent != null)
					continue;
				POLPackage package = PackageCache.GetPackage(node.Name);
				var materials_cfg_path = package.GetPackagedConfigPath("materials.cfg");
				if (materials_cfg_path == null) // File doesnt already exist.
					materials_cfg_path = package.path + @"\config\materials.cfg";
				if (!Directory.Exists(package.path + @"\config\"))
					Directory.CreateDirectory(package.path + @"\config\");
				ConfigFile config_file = ConfigRepository.global.LoadConfigFile(materials_cfg_path);
				ConfigRepository.WriteConfigFile(config_file);
			}
			
			MessageBox.Show("Done", "Materials.cfg", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		#endregion

		#region Tool On Material Tab Stuff
		public void PopulateToolOnMaterial()
		{
			foreach (POLTools.Package.POLPackage package in PackageCache.Global.packagelist)
			{
				string tom_cfg_path = package.GetPackagedConfigPath("toolOnMaterial.cfg");
				if (tom_cfg_path == null)
					continue;

				string nodename = ":" + package.name + ":toolOnMaterial.cfg";
				TreeNode pkg_node = toolonmaterial_treeview.Nodes.Add(package.name, nodename);
				ConfigFile tom_config = ConfigRepository.global.LoadConfigFile(tom_cfg_path);
				foreach (ConfigElem cfg_elem in tom_config.GetConfigElemRefs())
				{
					nodename = cfg_elem.name;
					
					pkg_node.Nodes.Add(nodename, nodename);
				}
			}
		}
		
		private void toolonmaterial_treeview_AfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeNode selected = toolonmaterial_treeview.SelectedNode;
			
			TB_toolonmaterial.Clear();
			combobox_tom_showmenus.Items.Clear();
			combobox_tom_showmenus.Text = "";
			foreach (Control control in groupBox6.Controls)
			{
				if (control is TextBox)
					((TextBox)control).Clear();
			}
			if (selected.Parent == null)
				return;

			ConfigElem tom_elem = ConfigRepository.global.FindElemInConfigFiles("toolOnMaterial.cfg", selected.Name);
						
			label17.Text = tom_elem.configfile.fullpath;
			foreach (string propname in tom_elem.ListConfigElemProperties())
			{
				foreach (string value in tom_elem.GetConfigStringList(propname))
				{
					TB_toolonmaterial.AppendText(propname + "	" + value + Environment.NewLine);
				}
			}

			toolonmaterial_tool_picture.Image = global::CraftTool.Properties.Resources.unused;
			if (tom_elem.PropertyExists("MenuScript"))
				TB_tom_menuscript.Text = tom_elem.GetConfigString("MenuScript");

			if (tom_elem.PropertyExists("ShowMenu"))
			{
				string value = tom_elem.GetConfigString("ShowMenu");
				int pos = combobox_tom_showmenus.Items.Add(value);
				combobox_tom_showmenus.SelectedIndex = pos;
			}
			combobox_tom_showmenus.Items.AddRange(ConfigRepository.global.GetElemNamesFromConfigFiles("CraftMenus.cfg").ToArray());
		}
		
		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			CreateConfigFileForPackage("toolOnMaterial.cfg");
		}

		private void toolStripMenuItem2_Click(object sender, EventArgs e)
		{
			TreeNode selected = CheckForSelectedNode(materials_tree_view);
			if (selected == null)
				return;
			TreeNode nodeparent = GetParentTreeNode(selected);

			Forms.SelectionPicker.SelectionPicker toolpicker = new Forms.SelectionPicker.SelectionPicker("Select a tool", ItemdescCache.Global.GetAllObjTypes());
			toolpicker.ShowDialog(this);
			if (toolpicker.result != DialogResult.OK)
				return;
			else
			{
				if (ItemdescCache.Global.GetElemForObjType(toolpicker.text) == null)
				{
					MessageBox.Show("Invalid object type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}
			List<string> categories = new List<string>();
			foreach (ConfigElem material_elem in ConfigRepository.global.GetElemsFromConfigFiles("materials.cfg"))
			{
				if (!material_elem.PropertyExists("Category"))
					continue;
				string category = material_elem.GetConfigString("Category");
				if (!categories.Exists(delegate(string n) { return n.ToLower() == category.ToLower(); }))
					categories.Add(category);
			}
			Forms.SelectionPicker.SelectionPicker catpicker = new Forms.SelectionPicker.SelectionPicker("Select a category", categories);
			catpicker.ShowDialog(this);
			if (catpicker.result != DialogResult.OK)
				return;
			else
			{
				if (!categories.Exists(delegate(string n) { return n.ToLower() == catpicker.text.ToLower(); }))
				{
					MessageBox.Show("Invalid category name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}
			string elem_name = "Tool=" + toolpicker.text + "&Material=" + catpicker.text;

			AddConfigElemForTreeNode(nodeparent, "toolOnMaterial.cfg", "MenuPointer", elem_name);
		}

		private void toolStripMenuItem3_Click(object sender, EventArgs e)
		{
			RemoveConfigTreeNode(toolonmaterial_treeview, "toolOnMaterial.cfg");
		}
		
		private void BTN_tom_writefiles_Click(object sender, EventArgs e)
		{

		}

		private void BTN_tom_update_Click(object sender, EventArgs e)
		{

		}
		#endregion
	}
}
