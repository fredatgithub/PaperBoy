namespace ManualPaperBoy
{
  partial class FormWait
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.labelWait = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // labelWait
      // 
      this.labelWait.AutoSize = true;
      this.labelWait.Dock = System.Windows.Forms.DockStyle.Fill;
      this.labelWait.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelWait.Location = new System.Drawing.Point(0, 0);
      this.labelWait.Name = "labelWait";
      this.labelWait.Size = new System.Drawing.Size(422, 36);
      this.labelWait.TabIndex = 0;
      this.labelWait.Text = "Please wait while downloading";
      this.labelWait.UseWaitCursor = true;
      // 
      // FormWait
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(443, 51);
      this.Controls.Add(this.labelWait);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormWait";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Please wait ....";
      this.UseWaitCursor = true;
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label labelWait;
  }
}