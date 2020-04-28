using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class updateMySQLFnCalculateJaro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder.ActiveProvider.ToLower().Contains("MySql".ToLower()))
            {
                migrationBuilder.Sql(@"
                                        DROP function IF EXISTS `fn_calculateJaro`;

                                        CREATE FUNCTION `fn_calculateJaro`( str1 VARCHAR(4000), str2 VARCHAR(4000)) RETURNS float
                                        READS SQL DATA
                                        DETERMINISTIC
                                        BEGIN
                                            DECLARE	Common1	VARCHAR(4000);
                                            DECLARE	Common2	VARCHAR(4000);
                                            DECLARE Common1_Len	INT;
                                            DECLARE	Common2_Len	INT;
                                            DECLARE s1_len	INT;
                                            DECLARE s2_len	INT;
                                            DECLARE	transpose_cnt	INT;
                                            DECLARE match_window	INT;
                                            DECLARE jaro_distance	FLOAT;
                                            SET	transpose_cnt	= 0;
                                            SET	match_window	= 0;
                                            SET	jaro_distance	= 0;
                                            Set s1_len = LENGTH( str1);
                                            Set s2_len = LENGTH( str2);
                                            SET	match_window = fn_calculateMatchWindow( s1_len, s2_len);
                                            SET	Common1 = fn_GetCommonCharacters( str1, str2, match_window);
                                            SET Common1_Len = LENGTH( Common1);

                                            IF Common1_Len = 0 OR LENGTH(Common1)=0 THEN
                                                RETURN 0;
                                            END IF;

                                            SET Common2 = fn_GetCommonCharacters( str2, str1, match_window);
                                            SET Common2_Len = LENGTH(Common2);
                                            IF Common1_Len <> Common2_Len OR LENGTH(Common2)=0 THEN
                                                RETURN 0;
                                            END IF;


                                            SET	transpose_cnt = fn_calculateTranspositions( Common1_Len, Common1, Common2);
                                            SET	jaro_distance =	Common1_Len / (3.0 * s1_len) +
                                            Common1_Len / (3.0 * s2_len) +
                                            ( Common1_Len - transpose_cnt) / (3.0 * Common1_Len);
                                            RETURN jaro_distance;

                                        END;
                            "); 
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
             
        }
    }
}
