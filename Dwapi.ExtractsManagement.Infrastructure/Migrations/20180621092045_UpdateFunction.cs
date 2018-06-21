using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class UpdateFunction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                  	DROP function IF EXISTS `fn_calculateTranspositionstion`;
                    DROP function IF EXISTS `fn_calculateTranspositions`;
					CREATE FUNCTION `fn_calculateTranspositions` (s1_len INT, str1 VARCHAR(4000), str2 VARCHAR(4000))
					RETURNS INTEGER
                    READS SQL DATA
                    DETERMINISTIC
					BEGIN
						DECLARE  transpositions INT;
						DECLARE  i INT;

						SET	 transpositions = 0;
						SET	 i = 0;
						WHILE  (i <  s1_len) DO	
							IF SUBSTRING( str1,  i+1, 1) <> SUBSTRING( str2,  i+1, 1) THEN
								SET	 transpositions =  transpositions + 1;
							END IF;
	
							SET  i =  i + 1 ;
						END  WHILE;

						SET	 transpositions =  transpositions / 2;
						RETURN  transpositions;

					END;");
            migrationBuilder.Sql(@"GRANT EXECUTE ON FUNCTION fn_calculateTranspositions TO '*'@'%';
            FLUSH PRIVILEGES;");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP function IF EXISTS `fn_calculateTranspositions`");
        }
    }
}
