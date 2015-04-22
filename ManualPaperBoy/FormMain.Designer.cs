namespace ManualPaperBoy
{
  partial class FormMain
  {
    /// <summary>
    /// Variable nécessaire au concepteur.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Nettoyage des ressources utilisées.
    /// </summary>
    /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Code généré par le Concepteur Windows Form

    /// <summary>
    /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
    /// le contenu de cette méthode avec l'éditeur de code.
    /// </summary>
    private void InitializeComponent()
    {
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.cancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.personalizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.frenchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.summaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.labelSaveFilePath = new System.Windows.Forms.Label();
      this.textBoxSaveFilePath = new System.Windows.Forms.TextBox();
      this.buttonPickDirectory = new System.Windows.Forms.Button();
      this.labelSelectEdition = new System.Windows.Forms.Label();
      this.comboBoxSelectEdition = new System.Windows.Forms.ComboBox();
      this.buttonSelectEdition = new System.Windows.Forms.Button();
      this.listBoxSelectedEdition = new System.Windows.Forms.ListBox();
      this.buttonDownloadEditions = new System.Windows.Forms.Button();
      this.labelSelectDate = new System.Windows.Forms.Label();
      this.dateTimePickerSelectDate = new System.Windows.Forms.DateTimePicker();
      this.buttonLaunchSelectedEdition = new System.Windows.Forms.Button();
      this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
      this.buttonRemove = new System.Windows.Forms.Button();
      this.groupBoxMultiSelectionDate = new System.Windows.Forms.GroupBox();
      this.dateTimePickerEndDate = new System.Windows.Forms.DateTimePicker();
      this.labelEndDate = new System.Windows.Forms.Label();
      this.dateTimePickerFromDate = new System.Windows.Forms.DateTimePicker();
      this.labelFromDate = new System.Windows.Forms.Label();
      this.radioButtonSeveralDates = new System.Windows.Forms.RadioButton();
      this.radioButtoSingleDate = new System.Windows.Forms.RadioButton();
      this.checkBoxEditionDuringWeekEnd = new System.Windows.Forms.CheckBox();
      this.buttonLaunchTargetDirectory = new System.Windows.Forms.Button();
      this.comboBoxSelectNewspaper = new System.Windows.Forms.ComboBox();
      this.labelSelectNewspaper = new System.Windows.Forms.Label();
      this.menuStrip1.SuspendLayout();
      this.groupBoxMultiSelectionDate.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.helpToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
      this.menuStrip1.Size = new System.Drawing.Size(877, 28);
      this.menuStrip1.TabIndex = 1;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.saveasToolStripMenuItem,
            this.toolStripSeparator1,
            this.printToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.toolStripSeparator2,
            this.quitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
      this.fileToolStripMenuItem.Text = "&Fichier";
      // 
      // newToolStripMenuItem
      // 
      this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.newToolStripMenuItem.Name = "newToolStripMenuItem";
      this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
      this.newToolStripMenuItem.Size = new System.Drawing.Size(247, 26);
      this.newToolStripMenuItem.Text = "&Nouveau";
      // 
      // openToolStripMenuItem
      // 
      this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.openToolStripMenuItem.Name = "openToolStripMenuItem";
      this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
      this.openToolStripMenuItem.Size = new System.Drawing.Size(247, 26);
      this.openToolStripMenuItem.Text = "&Ouvrir";
      // 
      // toolStripSeparator
      // 
      this.toolStripSeparator.Name = "toolStripSeparator";
      this.toolStripSeparator.Size = new System.Drawing.Size(244, 6);
      // 
      // saveToolStripMenuItem
      // 
      this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
      this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
      this.saveToolStripMenuItem.Size = new System.Drawing.Size(247, 26);
      this.saveToolStripMenuItem.Text = "&Enregistrer";
      // 
      // saveasToolStripMenuItem
      // 
      this.saveasToolStripMenuItem.Name = "saveasToolStripMenuItem";
      this.saveasToolStripMenuItem.Size = new System.Drawing.Size(247, 26);
      this.saveasToolStripMenuItem.Text = "Enregistrer &sous";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(244, 6);
      // 
      // printToolStripMenuItem
      // 
      this.printToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.printToolStripMenuItem.Name = "printToolStripMenuItem";
      this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
      this.printToolStripMenuItem.Size = new System.Drawing.Size(247, 26);
      this.printToolStripMenuItem.Text = "&Imprimer";
      // 
      // printPreviewToolStripMenuItem
      // 
      this.printPreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
      this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(247, 26);
      this.printPreviewToolStripMenuItem.Text = "Aperçu a&vant impression";
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(244, 6);
      // 
      // quitToolStripMenuItem
      // 
      this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
      this.quitToolStripMenuItem.Size = new System.Drawing.Size(247, 26);
      this.quitToolStripMenuItem.Text = "&Quitter";
      this.quitToolStripMenuItem.Click += new System.EventHandler(this.QuitToolStripMenuItem_Click);
      // 
      // editToolStripMenuItem
      // 
      this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cancelToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator3,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator4,
            this.selectAllToolStripMenuItem});
      this.editToolStripMenuItem.Name = "editToolStripMenuItem";
      this.editToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
      this.editToolStripMenuItem.Text = "&Edition";
      // 
      // cancelToolStripMenuItem
      // 
      this.cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
      this.cancelToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
      this.cancelToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
      this.cancelToolStripMenuItem.Text = "&Annuler";
      // 
      // redoToolStripMenuItem
      // 
      this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
      this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
      this.redoToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
      this.redoToolStripMenuItem.Text = "&Rétablir";
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(194, 6);
      // 
      // cutToolStripMenuItem
      // 
      this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
      this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
      this.cutToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
      this.cutToolStripMenuItem.Text = "&Couper";
      this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
      // 
      // copyToolStripMenuItem
      // 
      this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
      this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
      this.copyToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
      this.copyToolStripMenuItem.Text = "Co&pier";
      this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
      // 
      // pasteToolStripMenuItem
      // 
      this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
      this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
      this.pasteToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
      this.pasteToolStripMenuItem.Text = "Co&ller";
      this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(194, 6);
      // 
      // selectAllToolStripMenuItem
      // 
      this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
      this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
      this.selectAllToolStripMenuItem.Text = "Sélectio&nner tout";
      this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
      // 
      // toolsToolStripMenuItem
      // 
      this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.personalizeToolStripMenuItem,
            this.optionsToolStripMenuItem});
      this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
      this.toolsToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
      this.toolsToolStripMenuItem.Text = "&Outils";
      // 
      // personalizeToolStripMenuItem
      // 
      this.personalizeToolStripMenuItem.Name = "personalizeToolStripMenuItem";
      this.personalizeToolStripMenuItem.Size = new System.Drawing.Size(171, 26);
      this.personalizeToolStripMenuItem.Text = "&Personnaliser";
      // 
      // optionsToolStripMenuItem
      // 
      this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
      this.optionsToolStripMenuItem.Size = new System.Drawing.Size(171, 26);
      this.optionsToolStripMenuItem.Text = "&Options";
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frenchToolStripMenuItem,
            this.englishToolStripMenuItem});
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(86, 24);
      this.toolStripMenuItem1.Text = "Language";
      // 
      // frenchToolStripMenuItem
      // 
      this.frenchToolStripMenuItem.Name = "frenchToolStripMenuItem";
      this.frenchToolStripMenuItem.Size = new System.Drawing.Size(137, 26);
      this.frenchToolStripMenuItem.Text = "Français";
      // 
      // englishToolStripMenuItem
      // 
      this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
      this.englishToolStripMenuItem.Size = new System.Drawing.Size(137, 26);
      this.englishToolStripMenuItem.Text = "Anglais";
      // 
      // helpToolStripMenuItem
      // 
      this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.summaryToolStripMenuItem,
            this.indexToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.toolStripSeparator5,
            this.aboutToolStripMenuItem});
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new System.Drawing.Size(52, 24);
      this.helpToolStripMenuItem.Text = "&Aide";
      // 
      // summaryToolStripMenuItem
      // 
      this.summaryToolStripMenuItem.Name = "summaryToolStripMenuItem";
      this.summaryToolStripMenuItem.Size = new System.Drawing.Size(175, 26);
      this.summaryToolStripMenuItem.Text = "&Sommaire";
      // 
      // indexToolStripMenuItem
      // 
      this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
      this.indexToolStripMenuItem.Size = new System.Drawing.Size(175, 26);
      this.indexToolStripMenuItem.Text = "&Index";
      // 
      // searchToolStripMenuItem
      // 
      this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
      this.searchToolStripMenuItem.Size = new System.Drawing.Size(175, 26);
      this.searchToolStripMenuItem.Text = "&Rechercher";
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(172, 6);
      // 
      // aboutToolStripMenuItem
      // 
      this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
      this.aboutToolStripMenuItem.Size = new System.Drawing.Size(175, 26);
      this.aboutToolStripMenuItem.Text = "À &propos de...";
      this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
      // 
      // labelSaveFilePath
      // 
      this.labelSaveFilePath.AutoSize = true;
      this.labelSaveFilePath.Location = new System.Drawing.Point(51, 49);
      this.labelSaveFilePath.Name = "labelSaveFilePath";
      this.labelSaveFilePath.Size = new System.Drawing.Size(103, 17);
      this.labelSaveFilePath.TabIndex = 2;
      this.labelSaveFilePath.Text = "Save File Path:";
      // 
      // textBoxSaveFilePath
      // 
      this.textBoxSaveFilePath.Location = new System.Drawing.Point(183, 49);
      this.textBoxSaveFilePath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.textBoxSaveFilePath.Name = "textBoxSaveFilePath";
      this.textBoxSaveFilePath.Size = new System.Drawing.Size(605, 22);
      this.textBoxSaveFilePath.TabIndex = 3;
      this.textBoxSaveFilePath.Text = "C:\\Users\\User\\Documents\\perso\\magazine\\direct-matin";
      // 
      // buttonPickDirectory
      // 
      this.buttonPickDirectory.Location = new System.Drawing.Point(795, 50);
      this.buttonPickDirectory.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.buttonPickDirectory.Name = "buttonPickDirectory";
      this.buttonPickDirectory.Size = new System.Drawing.Size(37, 23);
      this.buttonPickDirectory.TabIndex = 4;
      this.buttonPickDirectory.Text = "...";
      this.buttonPickDirectory.UseVisualStyleBackColor = true;
      this.buttonPickDirectory.Click += new System.EventHandler(this.buttonPickDirectory_Click);
      // 
      // labelSelectEdition
      // 
      this.labelSelectEdition.AutoSize = true;
      this.labelSelectEdition.Location = new System.Drawing.Point(51, 145);
      this.labelSelectEdition.Name = "labelSelectEdition";
      this.labelSelectEdition.Size = new System.Drawing.Size(98, 17);
      this.labelSelectEdition.TabIndex = 5;
      this.labelSelectEdition.Text = "Select Edition:";
      // 
      // comboBoxSelectEdition
      // 
      this.comboBoxSelectEdition.FormattingEnabled = true;
      this.comboBoxSelectEdition.Location = new System.Drawing.Point(183, 142);
      this.comboBoxSelectEdition.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.comboBoxSelectEdition.Name = "comboBoxSelectEdition";
      this.comboBoxSelectEdition.Size = new System.Drawing.Size(233, 24);
      this.comboBoxSelectEdition.TabIndex = 6;
      // 
      // buttonSelectEdition
      // 
      this.buttonSelectEdition.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.buttonSelectEdition.Location = new System.Drawing.Point(430, 140);
      this.buttonSelectEdition.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.buttonSelectEdition.Name = "buttonSelectEdition";
      this.buttonSelectEdition.Size = new System.Drawing.Size(94, 31);
      this.buttonSelectEdition.TabIndex = 7;
      this.buttonSelectEdition.Text = "Select ->";
      this.buttonSelectEdition.UseVisualStyleBackColor = true;
      this.buttonSelectEdition.Click += new System.EventHandler(this.buttonSelectEdition_Click);
      // 
      // listBoxSelectedEdition
      // 
      this.listBoxSelectedEdition.FormattingEnabled = true;
      this.listBoxSelectedEdition.ItemHeight = 16;
      this.listBoxSelectedEdition.Location = new System.Drawing.Point(543, 100);
      this.listBoxSelectedEdition.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.listBoxSelectedEdition.Name = "listBoxSelectedEdition";
      this.listBoxSelectedEdition.Size = new System.Drawing.Size(288, 244);
      this.listBoxSelectedEdition.TabIndex = 8;
      // 
      // buttonDownloadEditions
      // 
      this.buttonDownloadEditions.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.buttonDownloadEditions.Location = new System.Drawing.Point(543, 349);
      this.buttonDownloadEditions.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.buttonDownloadEditions.Name = "buttonDownloadEditions";
      this.buttonDownloadEditions.Size = new System.Drawing.Size(288, 30);
      this.buttonDownloadEditions.TabIndex = 9;
      this.buttonDownloadEditions.Text = "Download selected editions";
      this.buttonDownloadEditions.UseVisualStyleBackColor = true;
      this.buttonDownloadEditions.Click += new System.EventHandler(this.buttonDownloadEditions_Click);
      // 
      // labelSelectDate
      // 
      this.labelSelectDate.AutoSize = true;
      this.labelSelectDate.Location = new System.Drawing.Point(51, 194);
      this.labelSelectDate.Name = "labelSelectDate";
      this.labelSelectDate.Size = new System.Drawing.Size(83, 17);
      this.labelSelectDate.TabIndex = 10;
      this.labelSelectDate.Text = "Select date:";
      // 
      // dateTimePickerSelectDate
      // 
      this.dateTimePickerSelectDate.Location = new System.Drawing.Point(183, 194);
      this.dateTimePickerSelectDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.dateTimePickerSelectDate.Name = "dateTimePickerSelectDate";
      this.dateTimePickerSelectDate.Size = new System.Drawing.Size(233, 22);
      this.dateTimePickerSelectDate.TabIndex = 11;
      // 
      // buttonLaunchSelectedEdition
      // 
      this.buttonLaunchSelectedEdition.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.buttonLaunchSelectedEdition.Location = new System.Drawing.Point(544, 381);
      this.buttonLaunchSelectedEdition.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.buttonLaunchSelectedEdition.Name = "buttonLaunchSelectedEdition";
      this.buttonLaunchSelectedEdition.Size = new System.Drawing.Size(288, 30);
      this.buttonLaunchSelectedEdition.TabIndex = 12;
      this.buttonLaunchSelectedEdition.Text = "Launch selected editions";
      this.buttonLaunchSelectedEdition.UseVisualStyleBackColor = true;
      this.buttonLaunchSelectedEdition.Click += new System.EventHandler(this.buttonLaunchSelectedEdition_Click);
      // 
      // buttonRemove
      // 
      this.buttonRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.buttonRemove.Location = new System.Drawing.Point(430, 194);
      this.buttonRemove.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.buttonRemove.Name = "buttonRemove";
      this.buttonRemove.Size = new System.Drawing.Size(94, 31);
      this.buttonRemove.TabIndex = 13;
      this.buttonRemove.Text = "<- Remove";
      this.buttonRemove.UseVisualStyleBackColor = true;
      this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
      // 
      // groupBoxMultiSelectionDate
      // 
      this.groupBoxMultiSelectionDate.Controls.Add(this.dateTimePickerEndDate);
      this.groupBoxMultiSelectionDate.Controls.Add(this.labelEndDate);
      this.groupBoxMultiSelectionDate.Controls.Add(this.dateTimePickerFromDate);
      this.groupBoxMultiSelectionDate.Controls.Add(this.labelFromDate);
      this.groupBoxMultiSelectionDate.Controls.Add(this.radioButtonSeveralDates);
      this.groupBoxMultiSelectionDate.Controls.Add(this.radioButtoSingleDate);
      this.groupBoxMultiSelectionDate.Location = new System.Drawing.Point(53, 263);
      this.groupBoxMultiSelectionDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.groupBoxMultiSelectionDate.Name = "groupBoxMultiSelectionDate";
      this.groupBoxMultiSelectionDate.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.groupBoxMultiSelectionDate.Size = new System.Drawing.Size(471, 178);
      this.groupBoxMultiSelectionDate.TabIndex = 14;
      this.groupBoxMultiSelectionDate.TabStop = false;
      this.groupBoxMultiSelectionDate.Text = "Time period";
      // 
      // dateTimePickerEndDate
      // 
      this.dateTimePickerEndDate.Location = new System.Drawing.Point(119, 149);
      this.dateTimePickerEndDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.dateTimePickerEndDate.Name = "dateTimePickerEndDate";
      this.dateTimePickerEndDate.Size = new System.Drawing.Size(233, 22);
      this.dateTimePickerEndDate.TabIndex = 16;
      // 
      // labelEndDate
      // 
      this.labelEndDate.AutoSize = true;
      this.labelEndDate.Location = new System.Drawing.Point(19, 154);
      this.labelEndDate.Name = "labelEndDate";
      this.labelEndDate.Size = new System.Drawing.Size(69, 17);
      this.labelEndDate.TabIndex = 17;
      this.labelEndDate.Text = "End date:";
      // 
      // dateTimePickerFromDate
      // 
      this.dateTimePickerFromDate.Location = new System.Drawing.Point(119, 108);
      this.dateTimePickerFromDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.dateTimePickerFromDate.Name = "dateTimePickerFromDate";
      this.dateTimePickerFromDate.Size = new System.Drawing.Size(233, 22);
      this.dateTimePickerFromDate.TabIndex = 15;
      this.dateTimePickerFromDate.Value = new System.DateTime(2015, 4, 15, 0, 0, 0, 0);
      // 
      // labelFromDate
      // 
      this.labelFromDate.AutoSize = true;
      this.labelFromDate.Location = new System.Drawing.Point(19, 113);
      this.labelFromDate.Name = "labelFromDate";
      this.labelFromDate.Size = new System.Drawing.Size(74, 17);
      this.labelFromDate.TabIndex = 15;
      this.labelFromDate.Text = "Start date:";
      // 
      // radioButtonSeveralDates
      // 
      this.radioButtonSeveralDates.AutoSize = true;
      this.radioButtonSeveralDates.Location = new System.Drawing.Point(21, 76);
      this.radioButtonSeveralDates.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.radioButtonSeveralDates.Name = "radioButtonSeveralDates";
      this.radioButtonSeveralDates.Size = new System.Drawing.Size(116, 21);
      this.radioButtonSeveralDates.TabIndex = 1;
      this.radioButtonSeveralDates.Text = "Several dates";
      this.radioButtonSeveralDates.UseVisualStyleBackColor = true;
      // 
      // radioButtoSingleDate
      // 
      this.radioButtoSingleDate.AutoSize = true;
      this.radioButtoSingleDate.Checked = true;
      this.radioButtoSingleDate.Location = new System.Drawing.Point(21, 41);
      this.radioButtoSingleDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.radioButtoSingleDate.Name = "radioButtoSingleDate";
      this.radioButtoSingleDate.Size = new System.Drawing.Size(157, 21);
      this.radioButtoSingleDate.TabIndex = 0;
      this.radioButtoSingleDate.TabStop = true;
      this.radioButtoSingleDate.Text = "Single selected date";
      this.radioButtoSingleDate.UseVisualStyleBackColor = true;
      // 
      // checkBoxEditionDuringWeekEnd
      // 
      this.checkBoxEditionDuringWeekEnd.AutoSize = true;
      this.checkBoxEditionDuringWeekEnd.Location = new System.Drawing.Point(55, 236);
      this.checkBoxEditionDuringWeekEnd.Margin = new System.Windows.Forms.Padding(4);
      this.checkBoxEditionDuringWeekEnd.Name = "checkBoxEditionDuringWeekEnd";
      this.checkBoxEditionDuringWeekEnd.Size = new System.Drawing.Size(205, 21);
      this.checkBoxEditionDuringWeekEnd.TabIndex = 15;
      this.checkBoxEditionDuringWeekEnd.Text = "Edition during the weekend.";
      this.checkBoxEditionDuringWeekEnd.UseVisualStyleBackColor = true;
      this.checkBoxEditionDuringWeekEnd.CheckedChanged += new System.EventHandler(this.checkBoxEditionDuringWeekEnd_CheckedChanged);
      // 
      // buttonLaunchTargetDirectory
      // 
      this.buttonLaunchTargetDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.buttonLaunchTargetDirectory.Location = new System.Drawing.Point(544, 412);
      this.buttonLaunchTargetDirectory.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.buttonLaunchTargetDirectory.Name = "buttonLaunchTargetDirectory";
      this.buttonLaunchTargetDirectory.Size = new System.Drawing.Size(288, 30);
      this.buttonLaunchTargetDirectory.TabIndex = 16;
      this.buttonLaunchTargetDirectory.Text = "Launch target directory";
      this.buttonLaunchTargetDirectory.UseVisualStyleBackColor = true;
      this.buttonLaunchTargetDirectory.Click += new System.EventHandler(this.buttonLaunchTargetDirectory_Click);
      // 
      // comboBoxSelectNewspaper
      // 
      this.comboBoxSelectNewspaper.FormattingEnabled = true;
      this.comboBoxSelectNewspaper.Location = new System.Drawing.Point(183, 100);
      this.comboBoxSelectNewspaper.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.comboBoxSelectNewspaper.Name = "comboBoxSelectNewspaper";
      this.comboBoxSelectNewspaper.Size = new System.Drawing.Size(341, 24);
      this.comboBoxSelectNewspaper.TabIndex = 18;
      // 
      // labelSelectNewspaper
      // 
      this.labelSelectNewspaper.AutoSize = true;
      this.labelSelectNewspaper.Location = new System.Drawing.Point(51, 100);
      this.labelSelectNewspaper.Name = "labelSelectNewspaper";
      this.labelSelectNewspaper.Size = new System.Drawing.Size(126, 17);
      this.labelSelectNewspaper.TabIndex = 17;
      this.labelSelectNewspaper.Text = "Select Newspaper:";
      // 
      // FormMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(877, 462);
      this.Controls.Add(this.comboBoxSelectNewspaper);
      this.Controls.Add(this.labelSelectNewspaper);
      this.Controls.Add(this.buttonLaunchTargetDirectory);
      this.Controls.Add(this.checkBoxEditionDuringWeekEnd);
      this.Controls.Add(this.groupBoxMultiSelectionDate);
      this.Controls.Add(this.buttonRemove);
      this.Controls.Add(this.buttonLaunchSelectedEdition);
      this.Controls.Add(this.dateTimePickerSelectDate);
      this.Controls.Add(this.labelSelectDate);
      this.Controls.Add(this.buttonDownloadEditions);
      this.Controls.Add(this.listBoxSelectedEdition);
      this.Controls.Add(this.buttonSelectEdition);
      this.Controls.Add(this.comboBoxSelectEdition);
      this.Controls.Add(this.labelSelectEdition);
      this.Controls.Add(this.buttonPickDirectory);
      this.Controls.Add(this.textBoxSaveFilePath);
      this.Controls.Add(this.labelSaveFilePath);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.Name = "FormMain";
      this.ShowIcon = false;
      this.Text = "Manual Paper Boy";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMainFormClosing);
      this.Load += new System.EventHandler(this.FormMain_Load);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.groupBoxMultiSelectionDate.ResumeLayout(false);
      this.groupBoxMultiSelectionDate.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
    private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveasToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem personalizeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem summaryToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem frenchToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
    private System.Windows.Forms.Label labelSaveFilePath;
    private System.Windows.Forms.TextBox textBoxSaveFilePath;
    private System.Windows.Forms.Button buttonPickDirectory;
    private System.Windows.Forms.Label labelSelectEdition;
    private System.Windows.Forms.ComboBox comboBoxSelectEdition;
    private System.Windows.Forms.Button buttonSelectEdition;
    private System.Windows.Forms.ListBox listBoxSelectedEdition;
    private System.Windows.Forms.Button buttonDownloadEditions;
    private System.Windows.Forms.Label labelSelectDate;
    private System.Windows.Forms.DateTimePicker dateTimePickerSelectDate;
    private System.Windows.Forms.Button buttonLaunchSelectedEdition;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    private System.Windows.Forms.Button buttonRemove;
    private System.Windows.Forms.GroupBox groupBoxMultiSelectionDate;
    private System.Windows.Forms.DateTimePicker dateTimePickerEndDate;
    private System.Windows.Forms.Label labelEndDate;
    private System.Windows.Forms.DateTimePicker dateTimePickerFromDate;
    private System.Windows.Forms.Label labelFromDate;
    private System.Windows.Forms.RadioButton radioButtonSeveralDates;
    private System.Windows.Forms.RadioButton radioButtoSingleDate;
    private System.Windows.Forms.CheckBox checkBoxEditionDuringWeekEnd;
    private System.Windows.Forms.Button buttonLaunchTargetDirectory;
    private System.Windows.Forms.ComboBox comboBoxSelectNewspaper;
    private System.Windows.Forms.Label labelSelectNewspaper;
  }
}