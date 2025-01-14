using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "badge",
                columns: table => new
                {
                    badge_id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    criteria = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    image_url = table.Column<string>(type: "varchar(2048)", unicode: false, maxLength: 2048, nullable: true),
                    points = table.Column<int>(type: "int", nullable: false, defaultValueSql: "('100')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__badge__E798965681CD864A", x => x.badge_id);
                });

            migrationBuilder.CreateTable(
                name: "course",
                columns: table => new
                {
                    cid = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    learning_objective = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    credit_points = table.Column<int>(type: "int", nullable: false),
                    difficulty_level = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "2"),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__course__D837D05F4B43937A", x => x.cid);
                });

            migrationBuilder.CreateTable(
                name: "leader_board",
                columns: table => new
                {
                    board_id = table.Column<int>(type: "int", nullable: false),
                    season = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "winter")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__leader_b__FB1C96E955016458", x => x.board_id);
                });

            migrationBuilder.CreateTable(
                name: "learning_goal",
                columns: table => new
                {
                    goal_id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "incomplete"),
                    deadline = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(datepart(year,getdate())+datepart(year,(1)))"),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, defaultValue: "yet to be determined")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__learning__76679A24113D1518", x => x.goal_id);
                });

            migrationBuilder.CreateTable(
                name: "notification",
                columns: table => new
                {
                    notification_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    time_stamp = table.Column<DateTime>(type: "datetime", nullable: true),
                    message = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    urgency = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "low")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__notifica__E059842FBCD13758", x => x.notification_id);
                });

            migrationBuilder.CreateTable(
                name: "quest",
                columns: table => new
                {
                    quest_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    difficulty_level = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true, defaultValue: "2"),
                    criteria = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, defaultValue: "pass the basics"),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, defaultValue: "none"),
                    title = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__quest__9A0F00CD7B2DD972", x => x.quest_id);
                });

            migrationBuilder.CreateTable(
                name: "reward",
                columns: table => new
                {
                    reward_id = table.Column<int>(type: "int", nullable: false),
                    value = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true, defaultValue: "none"),
                    type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "cash")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__reward__3DD599BC89DBB76D", x => x.reward_id);
                });

            migrationBuilder.CreateTable(
                name: "survey",
                columns: table => new
                {
                    survey_id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__survey__9DC31A0713FBFC68", x => x.survey_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    role = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__users__3213E83F6F783B7B", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "modules",
                columns: table => new
                {
                    module_id = table.Column<int>(type: "int", nullable: false),
                    cid = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    difficulty_level = table.Column<int>(type: "int", nullable: true, defaultValue: 2),
                    url = table.Column<string>(type: "varchar(2048)", unicode: false, maxLength: 2048, nullable: true, defaultValue: "none")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__modules__F7AE7B56ED56B1EB", x => new { x.module_id, x.cid });
                    table.ForeignKey(
                        name: "FK__modules__cid__5535A963",
                        column: x => x.cid,
                        principalTable: "course",
                        principalColumn: "cid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prerequisites_course",
                columns: table => new
                {
                    cid = table.Column<int>(type: "int", nullable: false),
                    prerequisite = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__prerequi__4CED9B75946F969F", x => new { x.cid, x.prerequisite });
                    table.ForeignKey(
                        name: "FK__prerequisit__cid__5070F446",
                        column: x => x.cid,
                        principalTable: "course",
                        principalColumn: "cid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "collaborative_quest",
                columns: table => new
                {
                    quest_id = table.Column<int>(type: "int", nullable: false),
                    deadline = table.Column<DateTime>(type: "datetime", nullable: true),
                    max_participants = table.Column<int>(type: "int", nullable: true, defaultValue: 5)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__collabor__9A0F00CDBF6EA3F6", x => x.quest_id);
                    table.ForeignKey(
                        name: "FK__collabora__quest__74794A92",
                        column: x => x.quest_id,
                        principalTable: "quest",
                        principalColumn: "quest_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "skill_mastery_quest",
                columns: table => new
                {
                    quest_id = table.Column<int>(type: "int", nullable: false),
                    skill = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__skill_ma__39FF22929089DF93", x => new { x.quest_id, x.skill });
                    table.ForeignKey(
                        name: "FK__skill_mas__quest__70A8B9AE",
                        column: x => x.quest_id,
                        principalTable: "quest",
                        principalColumn: "quest_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "survey_questions",
                columns: table => new
                {
                    survey_id = table.Column<int>(type: "int", nullable: false),
                    question = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__survey_q__7469363CFCFC671B", x => new { x.survey_id, x.question });
                    table.ForeignKey(
                        name: "FK__survey_qu__surve__4C6B5938",
                        column: x => x.survey_id,
                        principalTable: "survey",
                        principalColumn: "survey_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "admin",
                columns: table => new
                {
                    aid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__admin__DE508E2E83B730F6", x => x.aid);
                    table.ForeignKey(
                        name: "FK__admin__aid__3A81B327",
                        column: x => x.aid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "instructor",
                columns: table => new
                {
                    instructor_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    latest_qualification = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValue: "not-specified"),
                    expertise_area = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false, defaultValue: "not-specified")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__instruct__A1EF56E851CB9174", x => x.instructor_id);
                    table.ForeignKey(
                        name: "FK__instructo__instr__1DB06A4F",
                        column: x => x.instructor_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "learner",
                columns: table => new
                {
                    sid = table.Column<int>(type: "int", nullable: false),
                    fname = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    lname = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    gender = table.Column<bool>(type: "bit", nullable: true),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: true),
                    country = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    culture = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__learner__DDDFDD36DCF5400F", x => x.sid);
                    table.ForeignKey(
                        name: "FK__learner__sid__3D5E1FD2",
                        column: x => x.sid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "assessment",
                columns: table => new
                {
                    assessment_id = table.Column<int>(type: "int", nullable: false),
                    module_id = table.Column<int>(type: "int", nullable: false),
                    cid = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "quiz"),
                    total_marks = table.Column<int>(type: "int", nullable: false),
                    passing_marks = table.Column<int>(type: "int", nullable: false),
                    weightage = table.Column<int>(type: "int", nullable: false),
                    criteria = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true, defaultValue: "none"),
                    title = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__assessme__00B98C268A1BB57A", x => x.assessment_id);
                    table.ForeignKey(
                        name: "FK__assessment__66603565",
                        columns: x => new { x.module_id, x.cid },
                        principalTable: "modules",
                        principalColumns: new[] { "module_id", "cid" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "content_library",
                columns: table => new
                {
                    content_id = table.Column<int>(type: "int", nullable: false),
                    module_id = table.Column<int>(type: "int", nullable: false),
                    cid = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true, defaultValue: "none"),
                    metadata = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true, defaultValue: "none"),
                    type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "text"),
                    content_url = table.Column<string>(type: "varchar(2048)", unicode: false, maxLength: 2048, nullable: true, defaultValue: "none")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__content___655FE5104195A07F", x => x.content_id);
                    table.ForeignKey(
                        name: "FK__content_library__619B8048",
                        columns: x => new { x.module_id, x.cid },
                        principalTable: "modules",
                        principalColumns: new[] { "module_id", "cid" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "learning_activity",
                columns: table => new
                {
                    activity_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    module_id = table.Column<int>(type: "int", nullable: false),
                    cid = table.Column<int>(type: "int", nullable: false),
                    activity_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    instruction = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true, defaultValue: "none"),
                    max_points = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__learning__482FBD6375FEF9DA", x => x.activity_id);
                    table.ForeignKey(
                        name: "FK__learning_activit__6E01572D",
                        columns: x => new { x.module_id, x.cid },
                        principalTable: "modules",
                        principalColumns: new[] { "module_id", "cid" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "module_content",
                columns: table => new
                {
                    module_id = table.Column<int>(type: "int", nullable: false),
                    cid = table.Column<int>(type: "int", nullable: false),
                    content = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__module_c__4A34238F76ADAA7F", x => new { x.module_id, x.cid, x.content });
                    table.ForeignKey(
                        name: "FK__module_content__5812160E",
                        columns: x => new { x.module_id, x.cid },
                        principalTable: "modules",
                        principalColumns: new[] { "module_id", "cid" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "target_traits",
                columns: table => new
                {
                    module_id = table.Column<int>(type: "int", nullable: false),
                    cid = table.Column<int>(type: "int", nullable: false),
                    trait = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__target_t__CE042C4EDCA3AFFF", x => new { x.module_id, x.cid, x.trait });
                    table.ForeignKey(
                        name: "FK__target_traits__5AEE82B9",
                        columns: x => new { x.module_id, x.cid },
                        principalTable: "modules",
                        principalColumns: new[] { "module_id", "cid" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "disscussion_forum",
                columns: table => new
                {
                    forum_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    module_id = table.Column<int>(type: "int", nullable: false),
                    cid = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    last_activity = table.Column<DateTime>(type: "datetime", nullable: true),
                    time_stamp = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true, defaultValue: "none"),
                    InstructorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__disscuss__69A2FA583281A987", x => x.forum_id);
                    table.ForeignKey(
                        name: "FK__disscussion_foru__02C769E9",
                        columns: x => new { x.module_id, x.cid },
                        principalTable: "modules",
                        principalColumns: new[] { "module_id", "cid" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_disscussion_forum_instructor_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "instructor",
                        principalColumn: "instructor_id");
                });

            migrationBuilder.CreateTable(
                name: "teaches",
                columns: table => new
                {
                    instructor_id = table.Column<int>(type: "int", nullable: false),
                    cid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__teaches__4C6C2BED0D9F67CB", x => new { x.instructor_id, x.cid });
                    table.ForeignKey(
                        name: "FK__teaches__cid__37703C52",
                        column: x => x.cid,
                        principalTable: "course",
                        principalColumn: "cid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__teaches__instruc__367C1819",
                        column: x => x.instructor_id,
                        principalTable: "instructor",
                        principalColumn: "instructor_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "acheivement",
                columns: table => new
                {
                    acheivement_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sid = table.Column<int>(type: "int", nullable: false),
                    badge_id = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true, defaultValue: "none"),
                    date_earned = table.Column<DateTime>(type: "datetime", nullable: false),
                    type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "excellence")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__acheivem__9DEBB02A0EC15079", x => x.acheivement_id);
                    table.ForeignKey(
                        name: "FK__acheiveme__badge__65370702",
                        column: x => x.badge_id,
                        principalTable: "badge",
                        principalColumn: "badge_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__acheivement__sid__6442E2C9",
                        column: x => x.sid,
                        principalTable: "learner",
                        principalColumn: "sid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "completed_prerequisites",
                columns: table => new
                {
                    sid = table.Column<int>(type: "int", nullable: false),
                    cid = table.Column<int>(type: "int", nullable: false),
                    prerequisite = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__complete__991104818EDA5AA3", x => new { x.sid, x.cid, x.prerequisite });
                    table.ForeignKey(
                        name: "FK__completed_p__sid__0F2D40CE",
                        column: x => x.sid,
                        principalTable: "learner",
                        principalColumn: "sid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__completed_prereq__10216507",
                        columns: x => new { x.cid, x.prerequisite },
                        principalTable: "prerequisites_course",
                        principalColumns: new[] { "cid", "prerequisite" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "enrollment",
                columns: table => new
                {
                    enrollment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sid = table.Column<int>(type: "int", nullable: false),
                    cid = table.Column<int>(type: "int", nullable: false),
                    completion_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    enrollment_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "enrolled")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__enrollme__6D24AA7AC8EDA5D2", x => x.enrollment_id);
                    table.ForeignKey(
                        name: "FK__enrollment__cid__339FAB6E",
                        column: x => x.cid,
                        principalTable: "course",
                        principalColumn: "cid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__enrollment__sid__32AB8735",
                        column: x => x.sid,
                        principalTable: "learner",
                        principalColumn: "sid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "learner_mastery",
                columns: table => new
                {
                    sid = table.Column<int>(type: "int", nullable: false),
                    quest_id = table.Column<int>(type: "int", nullable: false),
                    completion_status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "incomplete")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__learner___147F2D3AF6FFC9B9", x => new { x.sid, x.quest_id });
                    table.ForeignKey(
                        name: "FK__learner_m__quest__7E02B4CC",
                        column: x => x.quest_id,
                        principalTable: "quest",
                        principalColumn: "quest_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__learner_mas__sid__7D0E9093",
                        column: x => x.sid,
                        principalTable: "learner",
                        principalColumn: "sid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "learners_collaboration",
                columns: table => new
                {
                    quest_id = table.Column<int>(type: "int", nullable: false),
                    sid = table.Column<int>(type: "int", nullable: false),
                    completetion_status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "incomplete")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__learners__E7D2FD1E1001A214", x => new { x.quest_id, x.sid });
                    table.ForeignKey(
                        name: "FK__learners___quest__7849DB76",
                        column: x => x.quest_id,
                        principalTable: "quest",
                        principalColumn: "quest_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__learners_co__sid__793DFFAF",
                        column: x => x.sid,
                        principalTable: "learner",
                        principalColumn: "sid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "learning_goals",
                columns: table => new
                {
                    goal_id = table.Column<int>(type: "int", nullable: false),
                    sid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__learning__0BBA67F73BEA2E8F", x => new { x.goal_id, x.sid });
                    table.ForeignKey(
                        name: "FK__learning___goal___46B27FE2",
                        column: x => x.goal_id,
                        principalTable: "learning_goal",
                        principalColumn: "goal_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__learning_go__sid__47A6A41B",
                        column: x => x.sid,
                        principalTable: "learner",
                        principalColumn: "sid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "learning_preferences",
                columns: table => new
                {
                    sid = table.Column<int>(type: "int", nullable: false),
                    preference = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__learning__DA46C0943BFCE533", x => new { x.sid, x.preference });
                    table.ForeignKey(
                        name: "FK__learning_pr__sid__4316F928",
                        column: x => x.sid,
                        principalTable: "learner",
                        principalColumn: "sid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personalization_profile",
                columns: table => new
                {
                    sid = table.Column<int>(type: "int", nullable: false),
                    order_number = table.Column<int>(type: "int", nullable: false),
                    prefered_content_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    emotional_state = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "sad"),
                    personality_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "curious")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__personal__2AEF3E7B58DCBB32", x => new { x.sid, x.order_number });
                    table.ForeignKey(
                        name: "FK__personaliza__sid__47DBAE45",
                        column: x => x.sid,
                        principalTable: "learner",
                        principalColumn: "sid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "quest_reward",
                columns: table => new
                {
                    reward_id = table.Column<int>(type: "int", nullable: false),
                    quest_id = table.Column<int>(type: "int", nullable: false),
                    sid = table.Column<int>(type: "int", nullable: false),
                    time_earned = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__quest_re__C3A8B66D7F854DC9", x => new { x.reward_id, x.quest_id, x.sid });
                    table.ForeignKey(
                        name: "FK__quest_rew__quest__0B5CAFEA",
                        column: x => x.quest_id,
                        principalTable: "quest",
                        principalColumn: "quest_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__quest_rew__rewar__0A688BB1",
                        column: x => x.reward_id,
                        principalTable: "reward",
                        principalColumn: "reward_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__quest_rewar__sid__0C50D423",
                        column: x => x.sid,
                        principalTable: "learner",
                        principalColumn: "sid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ranking",
                columns: table => new
                {
                    board_id = table.Column<int>(type: "int", nullable: false),
                    sid = table.Column<int>(type: "int", nullable: false),
                    cid = table.Column<int>(type: "int", nullable: false),
                    rank = table.Column<int>(type: "int", nullable: false),
                    total_marks = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ranking__86C16B3A8CA0D95A", x => new { x.board_id, x.sid });
                    table.ForeignKey(
                        name: "FK__ranking__board_i__3D2915A8",
                        column: x => x.board_id,
                        principalTable: "leader_board",
                        principalColumn: "board_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__ranking__cid__3F115E1A",
                        column: x => x.cid,
                        principalTable: "course",
                        principalColumn: "cid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__ranking__sid__3E1D39E1",
                        column: x => x.sid,
                        principalTable: "learner",
                        principalColumn: "sid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "recived_notification",
                columns: table => new
                {
                    notification_id = table.Column<int>(type: "int", nullable: false),
                    sid = table.Column<int>(type: "int", nullable: false),
                    is_read = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__recived___9D8479FC4AB793C3", x => new { x.notification_id, x.sid });
                    table.ForeignKey(
                        name: "FK__recived_n__notif__56E8E7AB",
                        column: x => x.notification_id,
                        principalTable: "notification",
                        principalColumn: "notification_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__recived_not__sid__57DD0BE4",
                        column: x => x.sid,
                        principalTable: "learner",
                        principalColumn: "sid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "skills",
                columns: table => new
                {
                    sid = table.Column<int>(type: "int", nullable: false),
                    skill = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__skills__7E2FFF696BDF99C9", x => new { x.sid, x.skill });
                    table.ForeignKey(
                        name: "FK__skills__sid__403A8C7D",
                        column: x => x.sid,
                        principalTable: "learner",
                        principalColumn: "sid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "survey_responses",
                columns: table => new
                {
                    response = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    survey_id = table.Column<int>(type: "int", nullable: false),
                    sid = table.Column<int>(type: "int", nullable: false),
                    question = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__survey_r__81E1CE1A64DB2471", x => new { x.survey_id, x.sid, x.question, x.response });
                    table.ForeignKey(
                        name: "FK__survey_resp__sid__503BEA1C",
                        column: x => x.sid,
                        principalTable: "learner",
                        principalColumn: "sid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__survey_responses__4F47C5E3",
                        columns: x => new { x.survey_id, x.question },
                        principalTable: "survey_questions",
                        principalColumns: new[] { "survey_id", "question" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "taken_assessment",
                columns: table => new
                {
                    assessment_id = table.Column<int>(type: "int", nullable: false),
                    sid = table.Column<int>(type: "int", nullable: false),
                    scored_points = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__taken_as__7D6471F5A30DD9F0", x => new { x.assessment_id, x.sid });
                    table.ForeignKey(
                        name: "FK__taken_ass__asses__693CA210",
                        column: x => x.assessment_id,
                        principalTable: "assessment",
                        principalColumn: "assessment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__taken_asses__sid__6A30C649",
                        column: x => x.sid,
                        principalTable: "learner",
                        principalColumn: "sid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "emotional_feedback",
                columns: table => new
                {
                    feedback_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sid = table.Column<int>(type: "int", nullable: false),
                    time_stamp = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    activity_id = table.Column<int>(type: "int", nullable: false),
                    emotional_state = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__emotiona__7A6B2B8CD8797F97", x => x.feedback_id);
                    table.ForeignKey(
                        name: "FK__emotional__activ__778AC167",
                        column: x => x.activity_id,
                        principalTable: "learning_activity",
                        principalColumn: "activity_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__emotional_f__sid__787EE5A0",
                        column: x => x.sid,
                        principalTable: "learner",
                        principalColumn: "sid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "interaction_log",
                columns: table => new
                {
                    log_id = table.Column<int>(type: "int", nullable: false),
                    activity_id = table.Column<int>(type: "int", nullable: false),
                    sid = table.Column<int>(type: "int", nullable: false),
                    time_stamp = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    action_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: true, defaultValue: 60)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__interact__9E2397E0C19BF7FA", x => x.log_id);
                    table.ForeignKey(
                        name: "FK__interacti__activ__72C60C4A",
                        column: x => x.activity_id,
                        principalTable: "learning_activity",
                        principalColumn: "activity_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__interaction__sid__73BA3083",
                        column: x => x.sid,
                        principalTable: "learner",
                        principalColumn: "sid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "learner_disscussion",
                columns: table => new
                {
                    forum_id = table.Column<int>(type: "int", nullable: false),
                    sid = table.Column<int>(type: "int", nullable: false),
                    post = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    time_posted = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LearnerSid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__learner___13E6176B9DE81B8F", x => new { x.forum_id, x.sid, x.post, x.time_posted });
                    table.ForeignKey(
                        name: "FK__learner_d__forum__0697FACD",
                        column: x => x.forum_id,
                        principalTable: "disscussion_forum",
                        principalColumn: "forum_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__learner_dis__sid__078C1F06",
                        column: x => x.sid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_learner_disscussion_learner_LearnerSid",
                        column: x => x.LearnerSid,
                        principalTable: "learner",
                        principalColumn: "sid");
                });

            migrationBuilder.CreateTable(
                name: "health_condition",
                columns: table => new
                {
                    sid = table.Column<int>(type: "int", nullable: false),
                    condition = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    order_number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__health_c__8CD72AB92DF42DBC", x => new { x.sid, x.order_number, x.condition });
                    table.ForeignKey(
                        name: "FK__health_condition__4AB81AF0",
                        columns: x => new { x.sid, x.order_number },
                        principalTable: "personalization_profile",
                        principalColumns: new[] { "sid", "order_number" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "learning_path",
                columns: table => new
                {
                    path_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sid = table.Column<int>(type: "int", nullable: false),
                    order_number = table.Column<int>(type: "int", nullable: false),
                    completion_status = table.Column<float>(type: "real", nullable: true, defaultValue: 0f),
                    custom_content = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__learning__464F800DD9332737", x => x.path_id);
                    table.ForeignKey(
                        name: "FK__learning_path__7C4F7684",
                        columns: x => new { x.sid, x.order_number },
                        principalTable: "personalization_profile",
                        principalColumns: new[] { "sid", "order_number" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "skill_progression",
                columns: table => new
                {
                    progression_id = table.Column<int>(type: "int", nullable: false),
                    proficiency_level = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValue: "newbie"),
                    sid = table.Column<int>(type: "int", nullable: false),
                    skill_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    time_stamp = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__skill_pr__F4059922D8A10E84", x => x.progression_id);
                    table.ForeignKey(
                        name: "FK__skill_progressio__5F7E2DAC",
                        columns: x => new { x.sid, x.skill_name },
                        principalTable: "skills",
                        principalColumns: new[] { "sid", "skill" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "emotional_feedback_review",
                columns: table => new
                {
                    feedback_id = table.Column<int>(type: "int", nullable: false),
                    instructor_id = table.Column<int>(type: "int", nullable: false),
                    feedback = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__emotiona__E075DEE26F61D688", x => new { x.feedback_id, x.instructor_id });
                    table.ForeignKey(
                        name: "FK__emotional__feedb__2CF2ADDF",
                        column: x => x.feedback_id,
                        principalTable: "emotional_feedback",
                        principalColumn: "feedback_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__emotional__instr__2DE6D218",
                        column: x => x.instructor_id,
                        principalTable: "instructor",
                        principalColumn: "instructor_id");
                });

            migrationBuilder.CreateTable(
                name: "learning_path_rules",
                columns: table => new
                {
                    apadtive_rule = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    path_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__learning__DF14AC0D2331AEF4", x => new { x.apadtive_rule, x.path_id });
                    table.ForeignKey(
                        name: "FK__learning___path___7F2BE32F",
                        column: x => x.path_id,
                        principalTable: "learning_path",
                        principalColumn: "path_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "path_review",
                columns: table => new
                {
                    instructor_id = table.Column<int>(type: "int", nullable: false),
                    path_id = table.Column<int>(type: "int", nullable: false),
                    feedback = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__path_rev__658BAEE86D5DD0F0", x => new { x.instructor_id, x.path_id });
                    table.ForeignKey(
                        name: "FK__path_revi__instr__245D67DE",
                        column: x => x.instructor_id,
                        principalTable: "instructor",
                        principalColumn: "instructor_id");
                    table.ForeignKey(
                        name: "FK__path_revi__path___25518C17",
                        column: x => x.path_id,
                        principalTable: "learning_path",
                        principalColumn: "path_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_acheivement",
                table: "acheivement",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "ix_acheivement3",
                table: "acheivement",
                column: "date_earned");

            migrationBuilder.CreateIndex(
                name: "ix_acheivement4",
                table: "acheivement",
                column: "sid");

            migrationBuilder.CreateIndex(
                name: "ix_acheivement5",
                table: "acheivement",
                column: "badge_id");

            migrationBuilder.CreateIndex(
                name: "ix_admin",
                table: "admin",
                column: "aid");

            migrationBuilder.CreateIndex(
                name: "ix_assessment",
                table: "assessment",
                column: "assessment_id");

            migrationBuilder.CreateIndex(
                name: "ix_assessment2",
                table: "assessment",
                column: "cid");

            migrationBuilder.CreateIndex(
                name: "ix_assessment3",
                table: "assessment",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "ix_assessment4",
                table: "assessment",
                columns: new[] { "module_id", "cid" });

            migrationBuilder.CreateIndex(
                name: "ix_badge",
                table: "badge",
                column: "criteria");

            migrationBuilder.CreateIndex(
                name: "ix_badge2",
                table: "badge",
                column: "title");

            migrationBuilder.CreateIndex(
                name: "ix_badge3",
                table: "badge",
                column: "points");

            migrationBuilder.CreateIndex(
                name: "ix_collaborative_quest",
                table: "collaborative_quest",
                column: "max_participants");

            migrationBuilder.CreateIndex(
                name: "ix_completed_prerequisites",
                table: "completed_prerequisites",
                column: "prerequisite");

            migrationBuilder.CreateIndex(
                name: "IX_completed_prerequisites_cid_prerequisite",
                table: "completed_prerequisites",
                columns: new[] { "cid", "prerequisite" });

            migrationBuilder.CreateIndex(
                name: "ix_completed_prerequisites2",
                table: "completed_prerequisites",
                column: "sid");

            migrationBuilder.CreateIndex(
                name: "ix_completed_prerequisites3",
                table: "completed_prerequisites",
                column: "cid");

            migrationBuilder.CreateIndex(
                name: "ix_content_library",
                table: "content_library",
                column: "content_id");

            migrationBuilder.CreateIndex(
                name: "IX_content_library_module_id_cid",
                table: "content_library",
                columns: new[] { "module_id", "cid" });

            migrationBuilder.CreateIndex(
                name: "ix_content_library2",
                table: "content_library",
                column: "title");

            migrationBuilder.CreateIndex(
                name: "ix_content_library3",
                table: "content_library",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "ix_content_library4",
                table: "content_library",
                column: "cid");

            migrationBuilder.CreateIndex(
                name: "ix_course",
                table: "course",
                column: "cid");

            migrationBuilder.CreateIndex(
                name: "ix_course2",
                table: "course",
                column: "title");

            migrationBuilder.CreateIndex(
                name: "ix_course3",
                table: "course",
                column: "credit_points");

            migrationBuilder.CreateIndex(
                name: "ix_disscussion_forum",
                table: "disscussion_forum",
                columns: new[] { "module_id", "cid" });

            migrationBuilder.CreateIndex(
                name: "IX_disscussion_forum_InstructorId",
                table: "disscussion_forum",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "ix_emotional_feedback",
                table: "emotional_feedback",
                column: "sid");

            migrationBuilder.CreateIndex(
                name: "IX_emotional_feedback_activity_id",
                table: "emotional_feedback",
                column: "activity_id");

            migrationBuilder.CreateIndex(
                name: "ix_emotional_feedback2",
                table: "emotional_feedback",
                column: "emotional_state");

            migrationBuilder.CreateIndex(
                name: "ix_emotional_feedback_review",
                table: "emotional_feedback_review",
                column: "feedback_id");

            migrationBuilder.CreateIndex(
                name: "ix_emotional_feedback_review2",
                table: "emotional_feedback_review",
                column: "instructor_id");

            migrationBuilder.CreateIndex(
                name: "ix_emotional_feedback_review3",
                table: "emotional_feedback_review",
                column: "feedback");

            migrationBuilder.CreateIndex(
                name: "ix_enrollment",
                table: "enrollment",
                column: "sid");

            migrationBuilder.CreateIndex(
                name: "ix_enrollment2",
                table: "enrollment",
                column: "cid");

            migrationBuilder.CreateIndex(
                name: "ix_enrollment3",
                table: "enrollment",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_enrollment4",
                table: "enrollment",
                column: "enrollment_date");

            migrationBuilder.CreateIndex(
                name: "ix_health_condition",
                table: "health_condition",
                column: "sid");

            migrationBuilder.CreateIndex(
                name: "ix_health_condition2",
                table: "health_condition",
                column: "condition");

            migrationBuilder.CreateIndex(
                name: "ix_health_condition3",
                table: "health_condition",
                column: "order_number");

            migrationBuilder.CreateIndex(
                name: "ix_instructor",
                table: "instructor",
                column: "instructor_id");

            migrationBuilder.CreateIndex(
                name: "ix_instructor2",
                table: "instructor",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_instructor3",
                table: "instructor",
                column: "expertise_area");

            migrationBuilder.CreateIndex(
                name: "ix_interaction_log2",
                table: "interaction_log",
                column: "action_type");

            migrationBuilder.CreateIndex(
                name: "ix_interaction_log5",
                table: "interaction_log",
                column: "sid");

            migrationBuilder.CreateIndex(
                name: "ix_interaction_log6",
                table: "interaction_log",
                column: "activity_id");

            migrationBuilder.CreateIndex(
                name: "ix_leader_board",
                table: "leader_board",
                column: "season");

            migrationBuilder.CreateIndex(
                name: "ix_learner",
                table: "learner",
                column: "country");

            migrationBuilder.CreateIndex(
                name: "ix_learner2",
                table: "learner",
                columns: new[] { "fname", "lname" });

            migrationBuilder.CreateIndex(
                name: "ix_learner3",
                table: "learner",
                column: "birth_date");

            migrationBuilder.CreateIndex(
                name: "ix_learner4",
                table: "learner",
                column: "sid");

            migrationBuilder.CreateIndex(
                name: "ix_learner_disscussion",
                table: "learner_disscussion",
                column: "post");

            migrationBuilder.CreateIndex(
                name: "IX_learner_disscussion_LearnerSid",
                table: "learner_disscussion",
                column: "LearnerSid");

            migrationBuilder.CreateIndex(
                name: "ix_learner_disscussion2",
                table: "learner_disscussion",
                column: "time_posted");

            migrationBuilder.CreateIndex(
                name: "ix_learner_disscussion3",
                table: "learner_disscussion",
                column: "sid");

            migrationBuilder.CreateIndex(
                name: "ix_learner_disscussion4",
                table: "learner_disscussion",
                column: "forum_id");

            migrationBuilder.CreateIndex(
                name: "ix_learner_mastery",
                table: "learner_mastery",
                column: "completion_status");

            migrationBuilder.CreateIndex(
                name: "IX_learner_mastery_quest_id",
                table: "learner_mastery",
                column: "quest_id");

            migrationBuilder.CreateIndex(
                name: "ix_learner_mastery2",
                table: "learner_mastery",
                column: "sid");

            migrationBuilder.CreateIndex(
                name: "ix_learners_collaboration",
                table: "learners_collaboration",
                column: "completetion_status");

            migrationBuilder.CreateIndex(
                name: "ix_learners_collaboration2",
                table: "learners_collaboration",
                column: "sid");

            migrationBuilder.CreateIndex(
                name: "ix_learning_activity",
                table: "learning_activity",
                column: "activity_id");

            migrationBuilder.CreateIndex(
                name: "ix_learning_activity2",
                table: "learning_activity",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "ix_learning_activity3",
                table: "learning_activity",
                column: "cid");

            migrationBuilder.CreateIndex(
                name: "ix_learning_activity4",
                table: "learning_activity",
                columns: new[] { "module_id", "cid" });

            migrationBuilder.CreateIndex(
                name: "ix_learning_goals",
                table: "learning_goals",
                column: "goal_id");

            migrationBuilder.CreateIndex(
                name: "ix_learning_goals2",
                table: "learning_goals",
                column: "sid");

            migrationBuilder.CreateIndex(
                name: "ix_learning_path",
                table: "learning_path",
                column: "sid");

            migrationBuilder.CreateIndex(
                name: "ix_learning_path2",
                table: "learning_path",
                column: "order_number");

            migrationBuilder.CreateIndex(
                name: "ix_learning_path3",
                table: "learning_path",
                column: "completion_status");

            migrationBuilder.CreateIndex(
                name: "ix_learning_path4",
                table: "learning_path",
                columns: new[] { "sid", "order_number" });

            migrationBuilder.CreateIndex(
                name: "ix_learning_path_rules",
                table: "learning_path_rules",
                column: "apadtive_rule");

            migrationBuilder.CreateIndex(
                name: "ix_learning_path_rules2",
                table: "learning_path_rules",
                column: "path_id");

            migrationBuilder.CreateIndex(
                name: "ix_learning_preferences",
                table: "learning_preferences",
                column: "sid");

            migrationBuilder.CreateIndex(
                name: "ix_learning_preferences2",
                table: "learning_preferences",
                column: "preference");

            migrationBuilder.CreateIndex(
                name: "ix_module_content",
                table: "module_content",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "ix_module_content2",
                table: "module_content",
                column: "content");

            migrationBuilder.CreateIndex(
                name: "ix_modules",
                table: "modules",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_modules_cid",
                table: "modules",
                column: "cid");

            migrationBuilder.CreateIndex(
                name: "ix_modules2",
                table: "modules",
                column: "title");

            migrationBuilder.CreateIndex(
                name: "ix_modules3",
                table: "modules",
                column: "difficulty_level");

            migrationBuilder.CreateIndex(
                name: "ix_notification",
                table: "notification",
                column: "urgency");

            migrationBuilder.CreateIndex(
                name: "ix_notification2",
                table: "notification",
                column: "time_stamp");

            migrationBuilder.CreateIndex(
                name: "ix_path_review",
                table: "path_review",
                column: "instructor_id");

            migrationBuilder.CreateIndex(
                name: "ix_path_review2",
                table: "path_review",
                column: "path_id");

            migrationBuilder.CreateIndex(
                name: "ix_personalization_profile",
                table: "personalization_profile",
                column: "sid");

            migrationBuilder.CreateIndex(
                name: "ix_personalization_profile2",
                table: "personalization_profile",
                column: "order_number");

            migrationBuilder.CreateIndex(
                name: "ix_personalization_profile3",
                table: "personalization_profile",
                column: "prefered_content_type");

            migrationBuilder.CreateIndex(
                name: "ix_personalization_profile4",
                table: "personalization_profile",
                column: "personality_type");

            migrationBuilder.CreateIndex(
                name: "ix_personalization_profile5",
                table: "personalization_profile",
                column: "emotional_state");

            migrationBuilder.CreateIndex(
                name: "ix_prerequisites_course",
                table: "prerequisites_course",
                column: "cid");

            migrationBuilder.CreateIndex(
                name: "ix_quest",
                table: "quest",
                column: "difficulty_level");

            migrationBuilder.CreateIndex(
                name: "ix_quest_reward2",
                table: "quest_reward",
                column: "reward_id");

            migrationBuilder.CreateIndex(
                name: "ix_quest_reward3",
                table: "quest_reward",
                column: "quest_id");

            migrationBuilder.CreateIndex(
                name: "ix_quest_reward4",
                table: "quest_reward",
                column: "sid");

            migrationBuilder.CreateIndex(
                name: "ix_ranking",
                table: "ranking",
                column: "board_id");

            migrationBuilder.CreateIndex(
                name: "ix_ranking2",
                table: "ranking",
                column: "sid");

            migrationBuilder.CreateIndex(
                name: "ix_ranking3",
                table: "ranking",
                column: "cid");

            migrationBuilder.CreateIndex(
                name: "ix_recived_notification",
                table: "recived_notification",
                column: "notification_id");

            migrationBuilder.CreateIndex(
                name: "ix_recived_notification2",
                table: "recived_notification",
                column: "sid");

            migrationBuilder.CreateIndex(
                name: "ix_reward",
                table: "reward",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "ix_skill_mastery_quest",
                table: "skill_mastery_quest",
                column: "quest_id");

            migrationBuilder.CreateIndex(
                name: "ix_skill_progression",
                table: "skill_progression",
                column: "proficiency_level");

            migrationBuilder.CreateIndex(
                name: "IX_skill_progression_sid_skill_name",
                table: "skill_progression",
                columns: new[] { "sid", "skill_name" });

            migrationBuilder.CreateIndex(
                name: "ix_skill_progression2",
                table: "skill_progression",
                column: "skill_name");

            migrationBuilder.CreateIndex(
                name: "ix_skill_progression3",
                table: "skill_progression",
                column: "time_stamp");

            migrationBuilder.CreateIndex(
                name: "ix_skill_progression4",
                table: "skill_progression",
                column: "sid");

            migrationBuilder.CreateIndex(
                name: "ix_skill_progression6",
                table: "skill_progression",
                columns: new[] { "skill_name", "sid" });

            migrationBuilder.CreateIndex(
                name: "idx_skills",
                table: "skills",
                column: "sid");

            migrationBuilder.CreateIndex(
                name: "idx_skills2",
                table: "skills",
                column: "skill");

            migrationBuilder.CreateIndex(
                name: "ix_survey",
                table: "survey",
                column: "title");

            migrationBuilder.CreateIndex(
                name: "ix_survey_questions",
                table: "survey_questions",
                column: "survey_id");

            migrationBuilder.CreateIndex(
                name: "ix_survey_questions2",
                table: "survey_questions",
                column: "question");

            migrationBuilder.CreateIndex(
                name: "ix_survey_responses",
                table: "survey_responses",
                column: "response");

            migrationBuilder.CreateIndex(
                name: "IX_survey_responses_survey_id_question",
                table: "survey_responses",
                columns: new[] { "survey_id", "question" });

            migrationBuilder.CreateIndex(
                name: "ix_survey_responses2",
                table: "survey_responses",
                column: "survey_id");

            migrationBuilder.CreateIndex(
                name: "ix_survey_responses3",
                table: "survey_responses",
                column: "sid");

            migrationBuilder.CreateIndex(
                name: "ix_survey_responses4",
                table: "survey_responses",
                column: "question");

            migrationBuilder.CreateIndex(
                name: "ix_taken_assessment",
                table: "taken_assessment",
                column: "assessment_id");

            migrationBuilder.CreateIndex(
                name: "ix_taken_assessment2",
                table: "taken_assessment",
                column: "sid");

            migrationBuilder.CreateIndex(
                name: "ix_taken_assessment3",
                table: "taken_assessment",
                column: "scored_points");

            migrationBuilder.CreateIndex(
                name: "ix_target_traits",
                table: "target_traits",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "ix_target_traits2",
                table: "target_traits",
                column: "trait");

            migrationBuilder.CreateIndex(
                name: "ix_target_traits3",
                table: "target_traits",
                column: "cid");

            migrationBuilder.CreateIndex(
                name: "ix_teaches",
                table: "teaches",
                column: "instructor_id");

            migrationBuilder.CreateIndex(
                name: "ix_teaches2",
                table: "teaches",
                column: "cid");

            migrationBuilder.CreateIndex(
                name: "ix_users",
                table: "users",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "ix_users2",
                table: "users",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "ix_users3",
                table: "users",
                column: "role");

            migrationBuilder.CreateIndex(
                name: "UQ__users__AB6E616420BDA166",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__users__F3DBC572938D7F34",
                table: "users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "acheivement");

            migrationBuilder.DropTable(
                name: "admin");

            migrationBuilder.DropTable(
                name: "collaborative_quest");

            migrationBuilder.DropTable(
                name: "completed_prerequisites");

            migrationBuilder.DropTable(
                name: "content_library");

            migrationBuilder.DropTable(
                name: "emotional_feedback_review");

            migrationBuilder.DropTable(
                name: "enrollment");

            migrationBuilder.DropTable(
                name: "health_condition");

            migrationBuilder.DropTable(
                name: "interaction_log");

            migrationBuilder.DropTable(
                name: "learner_disscussion");

            migrationBuilder.DropTable(
                name: "learner_mastery");

            migrationBuilder.DropTable(
                name: "learners_collaboration");

            migrationBuilder.DropTable(
                name: "learning_goals");

            migrationBuilder.DropTable(
                name: "learning_path_rules");

            migrationBuilder.DropTable(
                name: "learning_preferences");

            migrationBuilder.DropTable(
                name: "module_content");

            migrationBuilder.DropTable(
                name: "path_review");

            migrationBuilder.DropTable(
                name: "quest_reward");

            migrationBuilder.DropTable(
                name: "ranking");

            migrationBuilder.DropTable(
                name: "recived_notification");

            migrationBuilder.DropTable(
                name: "skill_mastery_quest");

            migrationBuilder.DropTable(
                name: "skill_progression");

            migrationBuilder.DropTable(
                name: "survey_responses");

            migrationBuilder.DropTable(
                name: "taken_assessment");

            migrationBuilder.DropTable(
                name: "target_traits");

            migrationBuilder.DropTable(
                name: "teaches");

            migrationBuilder.DropTable(
                name: "badge");

            migrationBuilder.DropTable(
                name: "prerequisites_course");

            migrationBuilder.DropTable(
                name: "emotional_feedback");

            migrationBuilder.DropTable(
                name: "disscussion_forum");

            migrationBuilder.DropTable(
                name: "learning_goal");

            migrationBuilder.DropTable(
                name: "learning_path");

            migrationBuilder.DropTable(
                name: "reward");

            migrationBuilder.DropTable(
                name: "leader_board");

            migrationBuilder.DropTable(
                name: "notification");

            migrationBuilder.DropTable(
                name: "quest");

            migrationBuilder.DropTable(
                name: "skills");

            migrationBuilder.DropTable(
                name: "survey_questions");

            migrationBuilder.DropTable(
                name: "assessment");

            migrationBuilder.DropTable(
                name: "learning_activity");

            migrationBuilder.DropTable(
                name: "instructor");

            migrationBuilder.DropTable(
                name: "personalization_profile");

            migrationBuilder.DropTable(
                name: "survey");

            migrationBuilder.DropTable(
                name: "modules");

            migrationBuilder.DropTable(
                name: "learner");

            migrationBuilder.DropTable(
                name: "course");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
