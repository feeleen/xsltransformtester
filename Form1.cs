using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using System.Text.RegularExpressions;

namespace XSLTransformTester
{
	public class Form1 : System.Windows.Forms.Form
	{
		#region Props


		private bool docComplete = false;

		private string XmlFile = "xml.xml";
		private string XslFile = "xsl.xsl";
		private string tpXmlInText = "Xml";
		private string tpXslInText = "Xsl";
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.MenuItem miTransform;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.TabControl tabControl2;
		private System.Windows.Forms.TabPage tpXmlIn;
		private ICSharpCode.TextEditor.TextEditorControl textBox1;
		private System.Windows.Forms.TabPage tpXslIn;
		private ICSharpCode.TextEditor.TextEditorControl textBox2;
		private System.Windows.Forms.Splitter splitter2;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tpXml;
        private ICSharpCode.TextEditor.TextEditorControl textBox3;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.StatusBarPanel StatePanel;
        private System.Windows.Forms.StatusBarPanel TransformPanel;
        private SplitContainer splitContainer1;
        private TabControl tabControl3;
        private TabPage tabPage2;
        private WebBrowser webBrowser1;
		private ContextMenuStrip contextMenuStrip1;
		private ToolStripMenuItem copyToolStripMenuItem;
		private ContextMenuStrip contextMenuStrip2;
		private ToolStripMenuItem copyToolStripMenuItem1;
		private ToolStripMenuItem pasteToolStripMenuItem;
		private ToolStripMenuItem pasteToolStripMenuItem1;
		private MenuItem menuItem10;
		private System.ComponentModel.IContainer components;

		#endregion

		public Form1()
		{
			InitializeComponent();
			InitializeTextEditors();
			OpenFiles();
		}
		
		
		public void InitializeTextEditors()
		{
			textBox1.Document.HighlightingStrategy = 
				HighlightingStrategyFactory.CreateHighlightingStrategy("XML");
			textBox1.Encoding = System.Text.Encoding.UTF8;
			textBox1.ShowEOLMarkers = false;
			textBox1.ShowSpaces = false;
			textBox1.ShowTabs = false;
			textBox1.EnableFolding = true;
			textBox1.ActiveTextAreaControl.TextArea.KeyPress += new KeyPressEventHandler(TextArea_KeyPress);

			textBox2.Document.HighlightingStrategy = 
				HighlightingStrategyFactory.CreateHighlightingStrategy("XML");
			textBox2.Encoding = System.Text.Encoding.UTF8;
			textBox2.ShowEOLMarkers = false;
			textBox2.ShowSpaces = false;
			textBox2.ShowTabs = false;
			textBox2.EnableFolding = true;
			textBox2.ActiveTextAreaControl.TextArea.KeyPress += new KeyPressEventHandler(TextArea_KeyPress);

			textBox3.Document.HighlightingStrategy = 
				HighlightingStrategyFactory.CreateHighlightingStrategy("XML");
			textBox3.Encoding = System.Text.Encoding.UTF8;
			textBox3.ShowEOLMarkers = false;
			textBox3.ShowSpaces = false;
			textBox3.ShowTabs = false;
			textBox3.EnableFolding = true;
		}



		public void OpenFiles()
		{
			try
			{
                bool fileExists = false;
				string path = Application.StartupPath + "/" + XmlFile;
				if (File.Exists(path))
				{
                    using (StreamReader sr = new StreamReader(path)) 
					{
						textBox1.Text = sr.ReadToEnd();
                        fileExists = true;
					}
				}
				path = Application.StartupPath + "/" + XslFile;
				if (File.Exists(path))
				{
					using (StreamReader sr = new StreamReader(path)) 
					{
						textBox2.Text = sr.ReadToEnd();
					    fileExists = true;
					}
				}

                if (!fileExists)
                {
                    textBox1.Text = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<catalog>
  <cd>
    <title>Empire Burlesque</title>
    <artist>Bob Dylan</artist>
    <country>USA</country>
    <company>Columbia</company>
    <price>10.90</price>
    <year>1985</year>
  </cd>
</catalog>";
                    textBox2.Text = @"<xsl:stylesheet version=""1.0"" xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"">

<xsl:template match=""/"">
  <html>
  <body>
  <h2>My CD Collection</h2>
  <table border=""1"">
    <tr bgcolor=""#9acd32"">
      <th>Title</th>
      <th>Artist</th>
    </tr>
    <xsl:for-each select=""catalog/cd"">
    <tr>
      <td><xsl:value-of select=""title""/></td>
      <td><xsl:value-of select=""artist""/></td>
    </tr>
    </xsl:for-each>
  </table>
  </body>
  </html>
</xsl:template>

</xsl:stylesheet>";
                }
			}
			catch(Exception ex)
			{
				Feeleen.ExceptionForm.ShowExceptionForm(ex);
			}

		}

		public void SaveFiles()
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				
				string path = Application.StartupPath + "/" + XmlFile;

				if (File.Exists(path)) 
				{
					File.Delete(path);
				}

				using (StreamWriter sw = new StreamWriter(path)) 
				{
					sw.Write(textBox1.Text);
					tpXmlIn.Text = tpXmlInText;
				}


				path = Application.StartupPath + "/" + XslFile;

				if (File.Exists(path)) 
				{
					File.Delete(path);
				}

				using (StreamWriter sw = new StreamWriter(path)) 
				{
					sw.Write(textBox2.Text);
					tpXslIn.Text = tpXslInText;
				}
			}
			catch(Exception ex)
			{
				Cursor.Current = Cursors.Default;
				MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		public void Transform()
		{
			Transform(false);
		}
			
		public void Transform(bool clean)
		{
			try
			{

				Cursor.Current = Cursors.WaitCursor;
				string sXml = textBox1.Text;
				string sXsl = textBox2.Text;

				//clean all except body
				if (clean && sXml.ToLower().IndexOf("<body") > 0)
				{
					sXml = sXml.Substring(sXml.ToLower().IndexOf("<body"));
					sXml = sXml.Substring(0, sXml.IndexOf("</body>") + 8);
					sXml = new Regex(@"(<script.+?</script>)").Replace(sXml, "");
					sXml = new Regex(@"(<!\[CDATA\[.+?\]\]>)").Replace(sXml, "");
					sXml = new Regex(@"(<!--.+?-->)").Replace(sXml, "");
				}
			

				XPathDocument xml = new XPathDocument(new StringReader(sXml));
				XPathDocument xsl = new XPathDocument(new StringReader(sXsl));

				//XslTransform xslt = new XslTransform();
				XslCompiledTransform xslt = new XslCompiledTransform();
				xslt.Load(xsl, null, null);
				using (MemoryStream xslOut = new MemoryStream())
				{
					using (XmlTextWriter wr = new XmlTextWriter(xslOut, Encoding.UTF8))
					{
						xslt.Transform(xml, null, wr, null);
						StreamReader sr = new StreamReader(xslOut, Encoding.UTF8);
						xslOut.Seek(0, SeekOrigin.Begin);
						try
						{
							textBox3.BeginUpdate();
							textBox3.ResetText();
							textBox3.Text = sr.ReadToEnd();
						}
						finally
						{
							textBox3.EndUpdate();
							textBox3.Refresh();
						}
						webBrowser1.DocumentText = textBox3.Text;
					}
				}
			}
			finally
			{
				Cursor.Current = Cursors.Default;
				TransformPanel.Text = "Transformation complete!";
			}
		}


		#region WinFormCode

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.miTransform = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.panel4 = new System.Windows.Forms.Panel();
			this.panel5 = new System.Windows.Forms.Panel();
			this.tabControl2 = new System.Windows.Forms.TabControl();
			this.tpXmlIn = new System.Windows.Forms.TabPage();
			this.textBox1 = new ICSharpCode.TextEditor.TextEditorControl();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.tpXslIn = new System.Windows.Forms.TabPage();
			this.textBox2 = new ICSharpCode.TextEditor.TextEditorControl();
			this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.copyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.panel2 = new System.Windows.Forms.Panel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tpXml = new System.Windows.Forms.TabPage();
			this.textBox3 = new ICSharpCode.TextEditor.TextEditorControl();
			this.tabControl3 = new System.Windows.Forms.TabControl();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.webBrowser1 = new System.Windows.Forms.WebBrowser();
			this.panel3 = new System.Windows.Forms.Panel();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.StatePanel = new System.Windows.Forms.StatusBarPanel();
			this.TransformPanel = new System.Windows.Forms.StatusBarPanel();
			this.panel4.SuspendLayout();
			this.panel5.SuspendLayout();
			this.tabControl2.SuspendLayout();
			this.tpXmlIn.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.tpXslIn.SuspendLayout();
			this.contextMenuStrip2.SuspendLayout();
			this.panel2.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tpXml.SuspendLayout();
			this.tabControl3.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.panel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.StatePanel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TransformPanel)).BeginInit();
			this.SuspendLayout();
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Red;
			this.imageList1.Images.SetKeyName(0, "");
			this.imageList1.Images.SetKeyName(1, "");
			this.imageList1.Images.SetKeyName(2, "");
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.miTransform,
            this.menuItem10,
            this.menuItem7});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem6,
            this.menuItem4,
            this.menuItem5,
            this.menuItem3,
            this.menuItem2});
			this.menuItem1.Text = "File";
			// 
			// menuItem6
			// 
			this.menuItem6.Enabled = false;
			this.menuItem6.Index = 0;
			this.menuItem6.Text = "Open";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 1;
			this.menuItem4.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.menuItem4.Text = "Save all";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Enabled = false;
			this.menuItem5.Index = 2;
			this.menuItem5.Text = "Save as...";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 3;
			this.menuItem3.Text = "-";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 4;
			this.menuItem2.Text = "Exit";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// miTransform
			// 
			this.miTransform.Index = 1;
			this.miTransform.Text = "Transform!";
			this.miTransform.Click += new System.EventHandler(this.miTransform_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 3;
			this.menuItem7.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem8});
			this.menuItem7.Text = "Help";
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 0;
			this.menuItem8.Text = "About";
			this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 2;
			this.menuItem10.Text = "Clean HTML and Transform!";
			this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.panel5);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel4.Location = new System.Drawing.Point(0, 0);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(768, 316);
			this.panel4.TabIndex = 3;
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.tabControl2);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel5.Location = new System.Drawing.Point(0, 0);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(768, 316);
			this.panel5.TabIndex = 1;
			// 
			// tabControl2
			// 
			this.tabControl2.Controls.Add(this.tpXmlIn);
			this.tabControl2.Controls.Add(this.tpXslIn);
			this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl2.HotTrack = true;
			this.tabControl2.ImageList = this.imageList1;
			this.tabControl2.ItemSize = new System.Drawing.Size(51, 19);
			this.tabControl2.Location = new System.Drawing.Point(0, 0);
			this.tabControl2.Name = "tabControl2";
			this.tabControl2.SelectedIndex = 0;
			this.tabControl2.Size = new System.Drawing.Size(768, 316);
			this.tabControl2.TabIndex = 1;
			// 
			// tpXmlIn
			// 
			this.tpXmlIn.Controls.Add(this.textBox1);
			this.tpXmlIn.ImageIndex = 1;
			this.tpXmlIn.Location = new System.Drawing.Point(4, 23);
			this.tpXmlIn.Name = "tpXmlIn";
			this.tpXmlIn.Padding = new System.Windows.Forms.Padding(4);
			this.tpXmlIn.Size = new System.Drawing.Size(760, 289);
			this.tpXmlIn.TabIndex = 0;
			this.tpXmlIn.Text = "Xml";
			// 
			// textBox1
			// 
			this.textBox1.ContextMenuStrip = this.contextMenuStrip1;
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox1.IsReadOnly = false;
			this.textBox1.Location = new System.Drawing.Point(4, 4);
			this.textBox1.Name = "textBox1";
			this.textBox1.ShowEOLMarkers = true;
			this.textBox1.ShowSpaces = true;
			this.textBox1.ShowTabs = true;
			this.textBox1.Size = new System.Drawing.Size(752, 281);
			this.textBox1.TabIndex = 0;
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_Changed);
			this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem1});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(103, 48);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// pasteToolStripMenuItem1
			// 
			this.pasteToolStripMenuItem1.Name = "pasteToolStripMenuItem1";
			this.pasteToolStripMenuItem1.Size = new System.Drawing.Size(102, 22);
			this.pasteToolStripMenuItem1.Text = "Paste";
			this.pasteToolStripMenuItem1.Click += new System.EventHandler(this.pasteToolStripMenuItem1_Click);
			// 
			// tpXslIn
			// 
			this.tpXslIn.Controls.Add(this.textBox2);
			this.tpXslIn.ImageIndex = 0;
			this.tpXslIn.Location = new System.Drawing.Point(4, 23);
			this.tpXslIn.Name = "tpXslIn";
			this.tpXslIn.Padding = new System.Windows.Forms.Padding(4);
			this.tpXslIn.Size = new System.Drawing.Size(760, 289);
			this.tpXslIn.TabIndex = 1;
			this.tpXslIn.Text = "Xsl";
			// 
			// textBox2
			// 
			this.textBox2.ContextMenuStrip = this.contextMenuStrip2;
			this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox2.IsReadOnly = false;
			this.textBox2.Location = new System.Drawing.Point(4, 4);
			this.textBox2.Name = "textBox2";
			this.textBox2.ShowEOLMarkers = true;
			this.textBox2.ShowSpaces = true;
			this.textBox2.ShowTabs = true;
			this.textBox2.Size = new System.Drawing.Size(752, 281);
			this.textBox2.TabIndex = 0;
			this.textBox2.Load += new System.EventHandler(this.textBox2_Load);
			this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
			// 
			// contextMenuStrip2
			// 
			this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem1,
            this.pasteToolStripMenuItem});
			this.contextMenuStrip2.Name = "contextMenuStrip2";
			this.contextMenuStrip2.Size = new System.Drawing.Size(103, 48);
			// 
			// copyToolStripMenuItem1
			// 
			this.copyToolStripMenuItem1.Name = "copyToolStripMenuItem1";
			this.copyToolStripMenuItem1.Size = new System.Drawing.Size(102, 22);
			this.copyToolStripMenuItem1.Text = "Copy";
			this.copyToolStripMenuItem1.Click += new System.EventHandler(this.copyToolStripMenuItem1_Click);
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
			this.pasteToolStripMenuItem.Text = "Paste";
			this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
			// 
			// splitter2
			// 
			this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter2.Location = new System.Drawing.Point(0, 316);
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size(768, 5);
			this.splitter2.TabIndex = 2;
			this.splitter2.TabStop = false;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.splitContainer1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 321);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(768, 152);
			this.panel2.TabIndex = 1;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabControl3);
			this.splitContainer1.Size = new System.Drawing.Size(768, 152);
			this.splitContainer1.SplitterDistance = 256;
			this.splitContainer1.TabIndex = 2;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tpXml);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.ImageList = this.imageList1;
			this.tabControl1.ItemSize = new System.Drawing.Size(86, 19);
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(256, 152);
			this.tabControl1.TabIndex = 1;
			// 
			// tpXml
			// 
			this.tpXml.Controls.Add(this.textBox3);
			this.tpXml.ImageIndex = 1;
			this.tpXml.Location = new System.Drawing.Point(4, 23);
			this.tpXml.Name = "tpXml";
			this.tpXml.Padding = new System.Windows.Forms.Padding(4);
			this.tpXml.Size = new System.Drawing.Size(248, 125);
			this.tpXml.TabIndex = 0;
			this.tpXml.Text = "Xml Output";
			// 
			// textBox3
			// 
			this.textBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox3.IsReadOnly = false;
			this.textBox3.Location = new System.Drawing.Point(4, 4);
			this.textBox3.Name = "textBox3";
			this.textBox3.ShowEOLMarkers = true;
			this.textBox3.ShowSpaces = true;
			this.textBox3.ShowTabs = true;
			this.textBox3.Size = new System.Drawing.Size(240, 117);
			this.textBox3.TabIndex = 0;
			// 
			// tabControl3
			// 
			this.tabControl3.Controls.Add(this.tabPage2);
			this.tabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl3.ImageList = this.imageList1;
			this.tabControl3.ItemSize = new System.Drawing.Size(86, 19);
			this.tabControl3.Location = new System.Drawing.Point(0, 0);
			this.tabControl3.Name = "tabControl3";
			this.tabControl3.SelectedIndex = 0;
			this.tabControl3.Size = new System.Drawing.Size(508, 152);
			this.tabControl3.TabIndex = 2;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.webBrowser1);
			this.tabPage2.ImageIndex = 2;
			this.tabPage2.Location = new System.Drawing.Point(4, 23);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
			this.tabPage2.Size = new System.Drawing.Size(500, 125);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Html Output";
			// 
			// webBrowser1
			// 
			this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webBrowser1.Location = new System.Drawing.Point(4, 4);
			this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser1.Name = "webBrowser1";
			this.webBrowser1.Size = new System.Drawing.Size(492, 117);
			this.webBrowser1.TabIndex = 0;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.panel4);
			this.panel3.Controls.Add(this.splitter2);
			this.panel3.Controls.Add(this.panel2);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(4, 4);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(768, 473);
			this.panel3.TabIndex = 3;
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(4, 477);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.StatePanel,
            this.TransformPanel});
			this.statusBar1.ShowPanels = true;
			this.statusBar1.Size = new System.Drawing.Size(768, 22);
			this.statusBar1.SizingGrip = false;
			this.statusBar1.TabIndex = 4;
			// 
			// StatePanel
			// 
			this.StatePanel.Name = "StatePanel";
			// 
			// TransformPanel
			// 
			this.TransformPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.TransformPanel.Name = "TransformPanel";
			this.TransformPanel.Width = 668;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(776, 503);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.statusBar1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Padding = new System.Windows.Forms.Padding(4);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "XSL Transform Tester";
			this.panel4.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.tabControl2.ResumeLayout(false);
			this.tpXmlIn.ResumeLayout(false);
			this.contextMenuStrip1.ResumeLayout(false);
			this.tpXslIn.ResumeLayout(false);
			this.contextMenuStrip2.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tpXml.ResumeLayout(false);
			this.tabControl3.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.StatePanel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TransformPanel)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			Transform();

		}

		private void htmlEditorControl_DocumentComplete(object sender, System.EventArgs e)
		{
			try
			{
				if (textBox3.Text != "")
				{
					//
					//htmlEditorControl.BodyHtml = textBox3.Text;
				}
				this.docComplete = true;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
			}
		}

		private void textBox2_Load(object sender, System.EventArgs e)
		{
		
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			if (MessageBox.Show("Сохранить исходные файлы?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				SaveFiles();

			Application.Exit();
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			SaveFiles();
			StatePanel.Text = "Files saved";
		}

		private void textBox1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			
		}

		private void textBox2_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			
		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			using (About a = new About() )
			{
				a.ShowDialog();
			}
		}


		private void miTransform_Click(object sender, System.EventArgs e)
		{
			Transform();
		}

		private void textBox1_Changed(object sender, System.EventArgs e)
		{
			StatePanel.Text = "";
			TransformPanel.Text = "";
		}

		private void TextArea_KeyPress(object sender, KeyPressEventArgs e)
		{
			StatePanel.Text = "";
			TransformPanel.Text = "";
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(
					textBox1.ActiveTextAreaControl.SelectionManager.SelectedText);
			
		}

		private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			
			
				textBox1.ActiveTextAreaControl.TextArea.InsertString(
				Clipboard.GetText());
		}

		private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(
						textBox2.ActiveTextAreaControl.SelectionManager.SelectedText);
		}

		private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			textBox2.ActiveTextAreaControl.TextArea.InsertString(
					Clipboard.GetText());
		}


		private void menuItem10_Click(object sender, EventArgs e)
		{
			Transform(true);
		}
	}
}
