namespace DawidKobierskiLab5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCourseNameIntoGrade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Grades", "CourseName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Grades", "CourseName");
        }
    }
}
