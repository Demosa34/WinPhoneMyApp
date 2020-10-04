namespace Example_algorithm
{
    partial class Error_Form
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
            this.F2_lb_error = new System.Windows.Forms.Label();
            this.F2_bt_Er_next = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // F2_lb_error
            // 
            this.F2_lb_error.AutoSize = true;
            this.F2_lb_error.Location = new System.Drawing.Point(94, 81);
            this.F2_lb_error.Name = "F2_lb_error";
            this.F2_lb_error.Size = new System.Drawing.Size(129, 13);
            this.F2_lb_error.TabIndex = 0;
            this.F2_lb_error.Text = "Введены не все данные";
            // 
            // F2_bt_Er_next
            // 
            this.F2_bt_Er_next.Location = new System.Drawing.Point(111, 191);
            this.F2_bt_Er_next.Name = "F2_bt_Er_next";
            this.F2_bt_Er_next.Size = new System.Drawing.Size(75, 23);
            this.F2_bt_Er_next.TabIndex = 1;
            this.F2_bt_Er_next.Text = "Ок";
            this.F2_bt_Er_next.UseVisualStyleBackColor = true;
            this.F2_bt_Er_next.Click += new System.EventHandler(this.F2_bt_Er_next_Click);
            // 
            // Error_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.F2_bt_Er_next);
            this.Controls.Add(this.F2_lb_error);
            this.Name = "Error_Form";
            this.Text = "Error";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label F2_lb_error;
        private System.Windows.Forms.Button F2_bt_Er_next;
    }
}