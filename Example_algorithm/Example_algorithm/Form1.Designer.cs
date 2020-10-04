namespace Example_algorithm
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lb_result = new System.Windows.Forms.Label();
            this.btn_Calculate = new System.Windows.Forms.Button();
            this.txbx_a = new System.Windows.Forms.MaskedTextBox();
            this.txbx_b = new System.Windows.Forms.MaskedTextBox();
            this.txbx_c = new System.Windows.Forms.MaskedTextBox();
            this.txbx_d = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(231, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "a =";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(231, 201);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "b =";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(231, 242);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "c =";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(231, 284);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "d =";
            // 
            // lb_result
            // 
            this.lb_result.AutoSize = true;
            this.lb_result.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_result.Location = new System.Drawing.Point(420, 28);
            this.lb_result.Name = "lb_result";
            this.lb_result.Size = new System.Drawing.Size(339, 25);
            this.lb_result.TabIndex = 9;
            this.lb_result.Text = "Введите значения для расчета...";
            // 
            // btn_Calculate
            // 
            this.btn_Calculate.Location = new System.Drawing.Point(339, 361);
            this.btn_Calculate.Name = "btn_Calculate";
            this.btn_Calculate.Size = new System.Drawing.Size(75, 23);
            this.btn_Calculate.TabIndex = 10;
            this.btn_Calculate.Text = "Вычислить";
            this.btn_Calculate.UseVisualStyleBackColor = true;
            this.btn_Calculate.Click += new System.EventHandler(this.btn_Calculate_Click);
            // 
            // txbx_a
            // 
            this.txbx_a.Location = new System.Drawing.Point(259, 153);
            this.txbx_a.Name = "txbx_a";
            this.txbx_a.Size = new System.Drawing.Size(100, 20);
            this.txbx_a.TabIndex = 12;
            // 
            // txbx_b
            // 
            this.txbx_b.Location = new System.Drawing.Point(259, 194);
            this.txbx_b.Name = "txbx_b";
            this.txbx_b.Size = new System.Drawing.Size(100, 20);
            this.txbx_b.TabIndex = 13;
            this.txbx_b.ValidatingType = typeof(int);
            // 
            // txbx_c
            // 
            this.txbx_c.Location = new System.Drawing.Point(259, 235);
            this.txbx_c.Name = "txbx_c";
            this.txbx_c.Size = new System.Drawing.Size(100, 20);
            this.txbx_c.TabIndex = 14;
            // 
            // txbx_d
            // 
            this.txbx_d.Location = new System.Drawing.Point(259, 281);
            this.txbx_d.Name = "txbx_d";
            this.txbx_d.Size = new System.Drawing.Size(100, 20);
            this.txbx_d.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(50, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(364, 25);
            this.label5.TabIndex = 16;
            this.label5.Text = "(min(a,b)+5*10)-max(10+d,7/1,c,a) = ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txbx_d);
            this.Controls.Add(this.txbx_c);
            this.Controls.Add(this.txbx_b);
            this.Controls.Add(this.txbx_a);
            this.Controls.Add(this.btn_Calculate);
            this.Controls.Add(this.lb_result);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lb_result;
        private System.Windows.Forms.Button btn_Calculate;
        private System.Windows.Forms.MaskedTextBox txbx_a;
        private System.Windows.Forms.MaskedTextBox txbx_b;
        private System.Windows.Forms.MaskedTextBox txbx_c;
        private System.Windows.Forms.MaskedTextBox txbx_d;
        private System.Windows.Forms.Label label5;
    }
}

