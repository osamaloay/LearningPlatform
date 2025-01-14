create database website;
use website;

create table users(
id int primary key identity,
username varchar(50) not null unique,
password varchar(50) not null,
email varchar(50) not null unique,
role varchar(50) not null,);
create index ix_users on users(username);
create index ix_users2 on users(email);
create index ix_users3 on users(role);

create table admin(
aid int not null,
foreign key (aid) references users on delete cascade on update cascade,
primary key(aid));
create index ix_admin on admin(aid);


create table learner(
sid int not null,
fname varchar(50) not null,
lname varchar(50)not null,
gender BIT,
birth_date date,
country varchar(50),
culture varchar(50),
foreign key (sid) references users on delete cascade on update cascade,
primary key(sid));
create index ix_learner on learner(country);
create index ix_learner4 on learner(sid);
create index ix_learner2 on learner(fname,lname);
create index ix_learner3 on learner(birth_date);

create table skills(
sid int not null,
skill varchar(50) not null,
FOREIGN KEY (sid) REFERENCES learner(sid) on delete cascade on update cascade,
PRIMARY KEY(sid,skill));
CREATE INDEX idx_skills on skills(sid);
CREATE INDEX idx_skills2 on skills(skill);



create table learning_preferences(
sid int not null,
preference varchar(50) not null,
FOREIGN KEY (sid) REFERENCES learner(sid) on delete cascade on update cascade,
PRIMARY KEY(sid,preference));
create index ix_learning_preferences on learning_preferences(sid);
create index ix_learning_preferences2 on learning_preferences(preference);



create table personalization_profile(
sid int not null,
order_number int not null,
prefered_content_type varchar(50) not null,
emotional_state varchar(50) default 'sad',
personality_type varchar(50) default 'curious',
FOREIGN KEY (sid) REFERENCES learner(sid) on delete cascade on update cascade,
PRIMARY KEY(sid,order_number));
create index ix_personalization_profile on personalization_profile(sid);
create index ix_personalization_profile2 on personalization_profile(order_number);
create index ix_personalization_profile3 on personalization_profile(prefered_content_type);
create index ix_personalization_profile4 on personalization_profile(personality_type);
create index ix_personalization_profile5 on personalization_profile(emotional_state);


create table health_condition(
sid int not null,
condition varchar(50) not null,
order_number int not null,
FOREIGN KEY (sid,order_number) REFERENCES personalization_profile(sid,order_number) on delete cascade on update cascade,
PRIMARY KEY(sid,order_number,condition));
create index ix_health_condition on health_condition(sid);
create index ix_health_condition2 on health_condition(condition);
create index ix_health_condition3 on health_condition(order_number);





create table course(
cid int primary key,
title varchar(50) not null,
learning_objective varchar(50) not null,
credit_points int not null,
difficulty_level varchar(50) default '2',
description varchar(100));
create index ix_course on course(cid);
create index ix_course2 on course(title);
create index ix_course3 on course(credit_points);


create table prerequisites_course(
cid int not null,
prerequisite varchar(100) not null,
FOREIGN KEY (cid) REFERENCES course(cid) on delete cascade on update cascade,
PRIMARY KEY(cid,prerequisite));
create index ix_prerequisites_course on prerequisites_course(cid);







create table modules(
module_id int not null, 
cid int not null, 
title varchar(50) not null,
difficulty_level int default 2,
url varchar(2048) default 'none',
FOREIGN KEY (cid) REFERENCES course(cid) on delete cascade on update cascade,
PRIMARY KEY(module_id,cid));
create index ix_modules on modules(module_id);
create index ix_modules2 on modules(title);
create index ix_modules3 on modules(difficulty_level);






create table module_content(
module_id int not null,
cid int not null,
content varchar(50) not null,
FOREIGN KEY (module_id,cid) REFERENCES modules(module_id,cid) on delete cascade on update cascade,
PRIMARY KEY(module_id,cid,content));
create index ix_module_content on module_content(module_id);
create index ix_module_content2 on module_content(content);


create table target_traits(
module_id int not null,
cid int not null,
trait varchar(50) not null,
FOREIGN KEY (module_id,cid) REFERENCES modules(module_id,cid) on delete cascade on update cascade,
PRIMARY KEY(module_id,cid,trait));
create index ix_target_traits on target_traits(module_id);
create index ix_target_traits2 on target_traits(trait);
create index ix_target_traits3 on target_traits(cid);
 

create table content_library(
content_id int primary key,
module_id int not null,
cid int not null,
title varchar(50) not null,
description varchar(100)default 'none',
metadata varchar(100)default 'none',
type varchar(50) default 'text',
content_url varchar(2048) default 'none',
FOREIGN KEY (module_id,cid) REFERENCES modules(module_id,cid) on delete cascade on update cascade);
create index ix_content_library on content_library(content_id);
create index ix_content_library2 on content_library(title);
create index ix_content_library3 on content_library(module_id);
create index ix_content_library4 on content_library(cid);


create table assessment(
assessment_id int not null,
module_id int not null,
cid int not null,
type varchar(50) default'quiz',
total_marks int not null,
passing_marks int not null,
weightage int not null,
criteria varchar(50) not null,
description varchar(100) default 'none',
title varchar(50) not null,
FOREIGN KEY (module_id,cid) REFERENCES modules(module_id,cid) on delete cascade on update cascade,
PRIMARY KEY(assessment_id));
create index ix_assessment on assessment(assessment_id);
create index ix_assessment2 on assessment(cid);
create index ix_assessment3 on assessment(module_id);
create index ix_assessment4 on assessment(module_id,cid);


create table taken_assessment(
assessment_id int not null,
sid int not null,
scored_points int not null,
foreign key (assessment_id) references assessment(assessment_id) on delete cascade on update cascade,
foreign key (sid) references learner(sid) on delete cascade on update cascade,
primary key(assessment_id,sid));
create index ix_taken_assessment on taken_assessment(assessment_id);
create index ix_taken_assessment2 on taken_assessment(sid);
create index ix_taken_assessment3 on taken_assessment(scored_points);



create table learning_activity(
activity_id int primary key identity,
module_id int not null,
cid int not null,
activity_type varchar(50) not null,
instruction varchar(100) default 'none',
max_points int not null,
FOREIGN KEY (module_id,cid) REFERENCES modules(module_id,cid) on delete cascade on update cascade);
create index ix_learning_activity on learning_activity(activity_id);
create index ix_learning_activity2 on learning_activity(module_id);
create index ix_learning_activity3 on learning_activity(cid);
create index ix_learning_activity4 on learning_activity(module_id,cid);

create table interaction_log(
log_id int primary key,
activity_id int not null,
sid int not null,
time_stamp datetime default current_timestamp,
action_type varchar(50) not null,
Duration int default 60,
FOREIGN KEY (activity_id) REFERENCES learning_activity(activity_id) on delete cascade on update cascade,
FOREIGN KEY (sid) REFERENCES learner(sid) on delete cascade on update cascade);
create index ix_interaction_log2 on interaction_log(action_type);
create index ix_interaction_log5 on interaction_log(sid);
create index ix_interaction_log6 on interaction_log(activity_id);


create table emotional_feedback(
feedback_id int primary key identity,
sid int not null,
time_stamp datetime default current_timestamp,
activity_id int not null,
emotional_state varchar(50) not null,
foreign key (activity_id) references learning_activity(activity_id) on delete cascade on update cascade,
FOREIGN KEY (sid) REFERENCES learner(sid) on delete cascade on update cascade);
create index ix_emotional_feedback on emotional_feedback(sid);
create index ix_emotional_feedback2 on emotional_feedback(emotional_state);



create table learning_path(
path_id int primary key identity,
sid int not null,
order_number int not null,
completion_status real default 0,
custom_content varchar(100),
foreign key (sid,order_number) references personalization_profile(sid,order_number) on delete cascade on update cascade);
create index ix_learning_path on learning_path(sid);
create index ix_learning_path2 on learning_path(order_number);
create index ix_learning_path3 on learning_path(completion_status);
create index ix_learning_path4 on learning_path(sid,order_number);


create table learning_path_rules(
apadtive_rule varchar(100) not null,
path_id int not null references learning_path on delete cascade on update cascade ,
Primary key(apadtive_rule,path_id));
create index ix_learning_path_rules on learning_path_rules(apadtive_rule);
create index ix_learning_path_rules2 on learning_path_rules(path_id);



create table instructor(
instructor_id int  primary key,
name varchar(50) not null,
latest_qualification varchar(50) not null default 'not-specified',
expertise_area varchar(40) not null default 'not-specified',
foreign key (instructor_id) references users(id) on delete cascade on update cascade);
create index ix_instructor on instructor(instructor_id);
create index ix_instructor2 on instructor(name);
create index ix_instructor3 on instructor(expertise_area);


create table path_review(
instructor_id int not null,
path_id int not null,
feedback varchar(100) not null,
foreign key (instructor_id) references instructor(instructor_id) on delete no action on update no action,
foreign key (path_id) references learning_path(path_id) on delete cascade on update cascade,
primary key(instructor_id,path_id));
create index ix_path_review on path_review(instructor_id);
create index ix_path_review2 on path_review(path_id);


create table emotional_feedback_review(
feedback_id int not null,
instructor_id int not null,
feedback varchar(100) not null,
foreign key (feedback_id) references emotional_feedback(feedback_id) on delete cascade on update cascade,
foreign key (instructor_id) references instructor(instructor_id) on delete no action on update no action,
primary key(feedback_id,instructor_id));
create index ix_emotional_feedback_review on emotional_feedback_review(feedback_id);
create index ix_emotional_feedback_review2 on emotional_feedback_review(instructor_id);
create index ix_emotional_feedback_review3 on emotional_feedback_review(feedback);

create table enrollment(
enrollment_id int primary key identity,
sid int not null,
cid int not null,
completion_date  datetime,
enrollment_date datetime not null default current_timestamp,
status varchar(50) default 'enrolled',
foreign key (sid) references learner(sid) on delete cascade on update cascade,
foreign key (cid) references course(cid) on delete cascade on update cascade);
create index ix_enrollment on enrollment(sid);
create index ix_enrollment2 on enrollment(cid);
create index ix_enrollment3 on enrollment(status);
create index ix_enrollment4 on enrollment(enrollment_date);



create table teaches(
instructor_id int not null,
cid int not null,
foreign key (instructor_id) references instructor(instructor_id) on delete cascade on update cascade,
foreign key (cid) references course(cid) on delete cascade on update cascade,
primary key(instructor_id,cid));
create index ix_teaches on teaches(instructor_id);
create index ix_teaches2 on teaches(cid);


create table leader_board(
board_id int primary key,
season varchar(50) default 'winter',
);
create index ix_leader_board on leader_board(season);


create table ranking(
board_id int not null,
sid int not null,
cid int not null,
rank int not null,
total_marks int not null,
foreign key (board_id) references leader_board(board_id) on delete cascade on update cascade,
foreign key (sid) references learner(sid) on delete cascade on update cascade,
foreign key (cid) references course(cid) on delete cascade on update cascade,
primary key(board_id,sid));

create index ix_ranking on ranking(board_id);
create index ix_ranking2 on ranking(sid);
create index ix_ranking3 on ranking(cid);


create table learning_goal(
goal_id int primary key,
status varchar(50) default 'incomplete',
deadline datetime default year(current_timestamp) + Year(1),
description varchar(100) not null default 'yet to be determined');


create table learning_goals(
goal_id int not null,
sid int not null,
foreign key (goal_id) references learning_goal(goal_id) on delete cascade on update cascade,
foreign key (sid) references learner(sid) on delete cascade on update cascade,
primary key(goal_id,sid));
create index ix_learning_goals on learning_goals(goal_id);
create index ix_learning_goals2 on learning_goals(sid);


create table survey(
survey_id int primary key,
title varchar(50) not null,
);
create index ix_survey on survey(title);


create table survey_questions(
survey_id int not null,
question varchar(100) not null,
foreign key (survey_id) references survey(survey_id) on delete cascade on update cascade,
primary key(survey_id,question));
create index ix_survey_questions on survey_questions(survey_id);
create index ix_survey_questions2 on survey_questions(question);


create table survey_responses(
response varchar(100) not null,
survey_id int not null,
sid int not null,
question varchar(100) not null,
FOREIGN KEY (survey_id,question) REFERENCES survey_questions(survey_id,question) on delete cascade on update cascade,
FOREIGN KEY (sid) REFERENCES learner(sid) on delete cascade on update cascade,
PRIMARY KEY(survey_id,sid,question,response));
create index ix_survey_responses on survey_responses(response);
create index ix_survey_responses2 on survey_responses(survey_id);
create index ix_survey_responses3 on survey_responses(sid);
create index ix_survey_responses4 on survey_responses(question);


create table notification(
notification_id int primary key identity,
time_stamp datetime,
message varchar(100) not null,
urgency varchar(50) default 'low',
);
create index ix_notification on notification(urgency);
create index ix_notification2 on notification(time_stamp);


create table recived_notification(
notification_id int not null,
sid int not null,
is_read bit default 0,
foreign key (notification_id) references notification(notification_id) on delete cascade on update cascade,
foreign key (sid) references learner(sid) on delete cascade on update cascade,
primary key(notification_id,sid));
create index ix_recived_notification on recived_notification(notification_id);
create index ix_recived_notification2 on recived_notification(sid);


create table badge(
badge_id int primary key,
title varchar(50) not null,
description varchar(100) not null,
criteria varchar(100) not null,
image_url varchar(2048),
points int not null default '100',
);
create index ix_badge on badge(criteria);
create index ix_badge2 on badge(title);
create index ix_badge3 on badge(points);


create table skill_progression(
progression_id int primary key,
proficiency_level varchar(50) not null default 'newbie',
sid int not null,
skill_name varchar(50) not null,
time_stamp datetime default current_timestamp,
Foreign key (sid,skill_name) references skills(sid,skill) on delete cascade on update cascade,
);
create index ix_skill_progression on skill_progression(proficiency_level);
create index ix_skill_progression2 on skill_progression(skill_name);
create index ix_skill_progression3 on skill_progression(time_stamp);
create index ix_skill_progression4 on skill_progression(sid);
create index ix_skill_progression6 on skill_progression(skill_name,sid);


create table acheivement(
acheivement_id int primary key identity ,
sid int not null,
badge_id int not null,
description varchar(100) default 'none',
date_earned datetime not null,
type varchar(50) default 'excellence',
foreign key (sid) references learner(sid) on delete cascade on update cascade,
foreign key (badge_id) references badge(badge_id) on delete cascade on update cascade,
);
create index ix_acheivement on acheivement(type);
create index ix_acheivement3 on acheivement(date_earned);
create index ix_acheivement4 on acheivement(sid);
create index ix_acheivement5 on acheivement(badge_id);



create table reward(
reward_id int primary key,
value int not null,
description varchar(100) default 'none',
type varchar(50) default 'cash',
);
create index ix_reward on reward(type);


create table quest(
quest_id int primary key identity,
difficulty_level varchar(100) default '2',
criteria varchar(100) not null default 'pass the basics',
description varchar(100) not null default 'none',
title varchar(100) not null,
);
create index ix_quest on quest(difficulty_level);




create table skill_mastery_quest(
quest_id int not null,
skill varchar(100) not null,
FOREIGN KEY (quest_id) REFERENCES quest(quest_id) on delete cascade on update cascade,
PRIMARY KEY(quest_id,skill));
create index ix_skill_mastery_quest on skill_mastery_quest(quest_id);


create table collaborative_quest(
quest_id int not null,
deadline datetime,
max_participants int default 5,
FOREIGN KEY (quest_id) REFERENCES quest(quest_id) on delete cascade on update cascade,
PRIMARY KEY(quest_id));
create index ix_collaborative_quest on collaborative_quest(max_participants);


create table learners_collaboration(
quest_id int not null,
sid int not null,
completetion_status varchar(50) default 'incomplete',
FOREIGN KEY (quest_id) REFERENCES quest(quest_id) on delete cascade on update cascade,
FOREIGN KEY (sid) REFERENCES learner(sid) on delete cascade on update cascade,
PRIMARY KEY(quest_id,sid));
create index ix_learners_collaboration on learners_collaboration(completetion_status);
create index ix_learners_collaboration2 on learners_collaboration(sid);


create table learner_mastery(
sid int not null,
completion_status varchar(50) default 'incomplete',
quest_id int not null,
FOREIGN KEY (sid) REFERENCES learner(sid) on delete cascade on update cascade,
FOREIGN KEY (quest_id) REFERENCES quest(quest_id) on delete cascade on update cascade,
PRIMARY KEY(sid,quest_id));

create index ix_learner_mastery on learner_mastery(completion_status);
create index ix_learner_mastery2 on learner_mastery(sid);


create table disscussion_forum(
forum_id int primary key identity,
module_id int not null,
cid int not null,
title varchar(50) not null,
last_activity datetime,
time_stamp datetime default current_timestamp,
description varchar(100) default 'none',
FOREIGN KEY (module_id,cid) REFERENCES modules(module_id,cid) on delete cascade on update cascade,
);
create index ix_disscussion_forum on disscussion_forum(module_id,cid);


create table learner_disscussion(
forum_id int not null,
sid int not null,
post varchar(200),
time_posted datetime default current_timestamp,
FOREIGN KEY (forum_id) REFERENCES disscussion_forum(forum_id) on delete cascade on update cascade,
FOREIGN KEY (sid) REFERENCES users(id) on delete cascade on update cascade,
PRIMARY KEY(forum_id,sid,post,time_posted));
create index ix_learner_disscussion on learner_disscussion(post);
create index ix_learner_disscussion2 on learner_disscussion(time_posted);
create index ix_learner_disscussion3 on learner_disscussion(sid);
create index ix_learner_disscussion4 on learner_disscussion(forum_id);



create table quest_reward(
reward_id int not null,
quest_id int not null,
sid int not null,
time_earned datetime,
FOREIGN KEY (reward_id) REFERENCES reward(reward_id) on delete cascade on update cascade,
FOREIGN KEY (quest_id) REFERENCES quest(quest_id) on delete cascade on update cascade,
FOREIGN KEY (sid) REFERENCES learner(sid) on delete cascade on update cascade,
PRIMARY KEY(reward_id,quest_id,sid));
create index ix_quest_reward2 on quest_reward(reward_id);
create index ix_quest_reward3 on quest_reward(quest_id);
create index ix_quest_reward4 on quest_reward(sid);


create table completed_prerequisites(
sid int not null,
cid int not null,
prerequisite varchar(100) not null,
FOREIGN KEY (sid) REFERENCES learner(sid) on delete cascade on update cascade,
FOREIGN KEY (cid,prerequisite) REFERENCES prerequisites_course(cid,prerequisite) on delete cascade on update cascade,
PRIMARY KEY(sid,cid,prerequisite));
create index ix_completed_prerequisites on completed_prerequisites(prerequisite);
create index ix_completed_prerequisites2 on completed_prerequisites(sid);
create index ix_completed_prerequisites3 on completed_prerequisites(cid);












-- admin procedures


--1
go
CREATE PROCEDURE ViewInfo
(@learnerID INT)
as 
begin 
select * from learner where sid = @learnerID
end

--2
go 
CREATE PROCEDURE LearnerInfo
(@learnerID INT)
as 
begin 
select * from personalization_profile
 where personalization_profile.sid = @learnerID
end


--3

go 
create procedure EmotionalState
(@LearnerID int,
@emotional_state varchar(50) output)
as
begin 
select p.emotional_state from 
personalization_profile  p
where sid = @LearnerID
order by 
order_number desc;
end


-- 4

go
Create procedure LogDetails (@learnerID int)
as 
begin
select * from interaction_log where sid = @learnerID
end

exec LogDetails 1;


--5


go
create procedure InstructorReview
(@InstructorID int)
as 
begin 
select * from emotional_feedback_review erev
inner join emotional_feedback ef on erev.feedback_id=ef.feedback_id where instructor_id = @InstructorID
end

exec InstructorReview 1;


--6
go
Create procedure CourseRemove (@courseID int)
as
begin
delete from course where cid = @courseID
end

select * from course


--7
go
Create procedure Highestgrade
as 
begin 
select a.assessment_id,a.cid ,a.criteria,a.description,a.module_id,a.passing_marks,
a.title,a.total_marks,a.type,a.weightage from assessment a 
inner join course c on a.cid = c.cid
where a.total_marks = (select max(total_marks) from assessment 
where cid = a.cid)
order by 
a.cid;
end

exec Highestgrade;

--8 
GO
CREATE PROCEDURE InstructorCount 
AS 
BEGIN 
    SELECT 
        c.cid,                       -- Course ID
        c.title,                     -- Course Title
        c.description,               -- Course Description
        c.difficulty_level,          -- Course Difficulty Level
        c.learning_objective,        -- Course Learning Objective
        c.credit_points,             -- Course Credit Points
        COUNT(DISTINCT t.instructor_id) AS instructor_count  -- Count of distinct instructors
    FROM 
        course c 
    INNER JOIN 
        teaches t ON c.cid = t.cid
    GROUP BY 
        c.cid, c.title, c.description, c.difficulty_level, c.learning_objective, c.credit_points
    HAVING 
        COUNT(DISTINCT t.instructor_id) > 1;  -- Filter for courses with more than one instructor
END
GO
exec InstructorCount;
select * from teaches
--insert into teaches values (2,1),(3,1),(3,2),(4,1);




--9
go
create procedure ViewNot
@learnerID INT
as 
BEGIN
select * from notification n 
inner join recived_notification rn on n.notification_id=rn.notification_id
where rn.sid = @learnerID
END
exec ViewNot 2;


--10
go 
create procedure CreateDiscussion
@moduleID int,
@courseID int,
@title varchar(50),
@description varchar(50)
as 
begin 
insert into disscussion_forum (module_id, cid, title, description)
values (@moduleID, @courseID, @title, @description)
print 'Discussion forum created successfully'
end
go
exec CreateDiscussion 1,1,'Discussion Forum','Discuss the basics of programming.'

--11

go 
create procedure RemoveBadge
@badgeID int
as 
begin
if exists (select * from badge where badge_id = @badgeID)
begin
delete from badge where badge_id = @badgeID
print 'Badge removed successfully'
end
else 
begin
print 'Badge does not exist'
end
end
exec RemoveBadge 15


--12 
go 
create procedure CriteriaDelete
@criteria varchar(50)
as 
begin 
if exists (select * from quest where criteria = @criteria)
begin
delete from quest where criteria = @criteria
end
else
begin
print 'Criteria does not exist'
end
end
select * from quest
exec CriteriaDelete 'Complete all tasks'

--13
go
create procedure NotificationUpdate
@LearnerID int,
@notificationID int,
@ReadStatus bit
as 
begin 
if(@ReadStatus=1)
begin
update recived_notification
set is_read = @ReadStatus
where sid = @LearnerID and notification_id = @notificationID
end
else 
begin
delete from recived_notification
where sid = @LearnerID and notification_id = @notificationID
end
end

exec NotificationUpdate 2,1,0
select * from recived_notification



go
create procedure EmotionalTrendAnalysis 
@CourseID int,
@ModuleID int,
@TimePeriod datetime
as 
begin
select sid,emotional_state from emotional_feedback ef
inner join learning_activity la on ef.activity_id=la.activity_id
where la.cid = @CourseID and la.module_id = @ModuleID and ef.time_stamp >= @TimePeriod
group by 
ef.sid,ef.emotional_state
order by 
ef.sid
end





-- 2 Learner procedures 


--1 
GO
CREATE PROCEDURE ProfileUpdate
    @learnerID INT, 
    @ProfileID INT,
    @PreferedContentType VARCHAR(50),
    @emotional_state VARCHAR(50), 
    @PersonalityType VARCHAR(50)
AS 
BEGIN 
    -- Check if the profile exists
    IF EXISTS (SELECT * FROM personalization_profile WHERE sid = @learnerID AND order_number = @ProfileID)
    BEGIN
        -- Update the existing profile
        UPDATE personalization_profile
        SET 
            prefered_content_type = @PreferedContentType,
            emotional_state = @emotional_state,
            personality_type = @PersonalityType
        WHERE 
            sid = @learnerID AND order_number = @ProfileID;
    END
    ELSE  
    BEGIN
        -- Insert a new profile
        INSERT INTO personalization_profile (sid, order_number, prefered_content_type, emotional_state, personality_type)
        VALUES (@learnerID, @ProfileID, @PreferedContentType, @emotional_state, @PersonalityType);
    END
END
GO

--2 
GO
CREATE PROCEDURE TotalPoints
    @learnerID INT,
    @RewardType VARCHAR(50),
    @TotalPoints INT OUTPUT
AS
BEGIN
    -- Initialize @TotalPoints to 0 to handle cases where no rewards are found
    SET @TotalPoints = 0;

    -- Calculate the total points
    SELECT @TotalPoints = SUM(r.value)
    FROM reward r 
    INNER JOIN quest_reward qr ON r.reward_id = qr.reward_id
    INNER JOIN quest q ON qr.quest_id = q.quest_id
    WHERE qr.sid = @learnerID AND r.type = @RewardType;

    -- Optionally, you could return a message if no points were found
    IF @TotalPoints IS NULL
    BEGIN
        SET @TotalPoints = 0; -- Ensure it returns 0 if no points are found
    END
END
GO

--3

GO
CREATE PROCEDURE EnrolledCourses
	@learnerID INT
    as
    begin 
    select c.cid, c.title, c.description, c.difficulty_level, c.learning_objective, c.credit_points
    from course c
    inner join enrollment e on c.cid = e.cid
	where e.sid = @learnerID
	end

exec EnrolledCourses 1;


--4
GO
CREATE PROCEDURE Prerequisites
    @CourseID INT,
    @LearnerID INT
AS 
BEGIN 
    -- Check if there are any prerequisites for the specified course
    DECLARE @TotalPrerequisites INT;
    DECLARE @CompletedPrerequisites INT;

    -- Count total prerequisites for the specified course
    SELECT @TotalPrerequisites = COUNT(*)
    FROM prerequisites_course
    WHERE cid = @CourseID;

    -- Count completed prerequisites for the learner
    SELECT @CompletedPrerequisites = COUNT(*)
    FROM completed_prerequisites cp
    WHERE cp.cid = @CourseID AND cp.sid = @LearnerID;

    -- Check if the learner has completed all prerequisites
    IF @TotalPrerequisites = @CompletedPrerequisites AND @TotalPrerequisites > 0
    BEGIN
        SELECT 'All prerequisites are completed.' AS Result;
    END
    ELSE
    BEGIN
        SELECT 'Not all prerequisites are completed.' AS Result;
    END
END
GO






--5
GO
CREATE PROCEDURE Moduletraits
    @TargetTrait VARCHAR(50),
    @CourseID INT
AS
BEGIN
    -- Find the module with the specified trait in the given course
    SELECT 
        m.module_id, 
        m.title, 
        m.cid,
        m.url,
        m.difficulty_level
    FROM 
        modules m 
    INNER JOIN 
        target_traits tt ON m.module_id = tt.module_id
    WHERE 
        m.cid = @CourseID 
        AND tt.trait = @TargetTrait;
END
GO




--6
go 
create procedure LeaderboardRank
@LeaderboardID int

as
begin

select rank,fname,lname,country,season from leader_board lb inner join 
ranking r on r.board_id  = lb.board_id
inner join learner l on r.sid = l.sid
where lb.board_id = @LeaderboardID

end

exec LeaderboardRank 1;




--7
go 
create procedure ViewMyDeviceCharge
@ActivityID int,
@LearnerID int,
@timestamp time,
@emotionalstate varchar(50)
as
begin
insert into emotional_feedback 
( sid, time_stamp, activity_id, emotional_state)
values 
( @LearnerID, @timestamp,@ActivityID, @emotionalstate)
end



exec ViewMyDeviceCharge 1,1,'10:30:00.000','Happy'



--8 
GO
CREATE PROCEDURE JoinQuest
    @QuestID INT,
    @LearnerID INT
AS
BEGIN
    DECLARE @MaxParticipants INT;
    DECLARE @CurrentParticipants INT;

    -- Get the maximum number of participants for the quest
    SELECT @MaxParticipants = max_participants 
    FROM collaborative_quest 
    WHERE quest_id = @QuestID;

    -- Get the current number of participants for the quest
    SELECT @CurrentParticipants = COUNT(*) 
    FROM learners_collaboration lc
    INNER JOIN quest q ON lc.quest_id = q.quest_id
    WHERE q.quest_id = @QuestID;

    -- Check if the current number of participants is less than the maximum allowed
    IF @CurrentParticipants < @MaxParticipants
    BEGIN
        -- Logic to add the learner to the quest
        INSERT INTO learners_collaboration (quest_id, sid) 
        VALUES (@QuestID, @LearnerID);

        print 'Learner successfully joined the quest.' 
    END
    ELSE
    BEGIN
        print 'The quest is already full. Cannot join.' ;
    END
END
GO

--9

go 
create procedure SkillsProfeciency
@LearnerID int
as
begin
select s.skill,sp.proficiency_level from skills s inner join skill_progression sp on s.sid=sp.sid and s.skill=sp.skill_name
where sp.sid = @LearnerID
end

select * from skill_progression
exec SkillsProfeciency 1;



--10

go 
create procedure Viewscore
@LearnerID int,
@AssessmentID int,
@score int output
as 
begin
select @score = ta.scored_points from taken_assessment ta where sid = @LearnerID and assessment_id = @AssessmentID
end



select * from taken_assessment
declare @score int
exec Viewscore 1, 1, @score output
select @score as Score;


--11
go 
create procedure AssessmentsList 
@CourseID int,
@LearnerID int
as 
begin
select ta.scored_points ,a.assessment_id,a.criteria,a.description,a.module_id,a.passing_marks,a.title,a.total_marks,a.type,a.weightage from taken_assessment ta 
inner join assessment a on ta.assessment_id = a.assessment_id
where ta.sid = @LearnerID and a.cid = @CourseID
end






--12

GO
CREATE PROCEDURE Courseregister
    @LearnerID INT,
    @CourseID INT
AS
BEGIN
    -- Check if the learner has completed all prerequisites for the course
    IF NOT EXISTS (
        SELECT * 
        FROM prerequisites_course pc 
        WHERE pc.cid = @CourseID 
        AND NOT EXISTS (
            SELECT * 
            FROM completed_prerequisites cp 
            WHERE cp.sid = @LearnerID 
            AND cp.cid = pc.cid
        )
    )
    BEGIN
        -- Insert the enrollment record if prerequisites are completed
        IF NOT EXISTS (
            SELECT * 
            FROM enrollment 
            WHERE sid = @LearnerID 
            AND cid = @CourseID
        )
        BEGIN
            INSERT INTO enrollment (sid, cid) VALUES (@LearnerID, @CourseID);
            PRINT 'Course registered successfully';
        END
        ELSE
        BEGIN
            PRINT 'Learner is already enrolled in the course.';
        END
    END
    ELSE
    BEGIN
        PRINT 'Prerequisites not completed';
    END
END
GO





--- now
go 
create procedure Post
@LearnerID int,
@DiscussionID int,
@Post varchar(max)
as
begin
IF exists ( select * from learner_disscussion where forum_id = @DiscussionID )
begin
insert into learner_disscussion (forum_id, sid, post) values (@DiscussionID, @LearnerID, @Post)
end
else
begin
print 'Discussion forum does not exist'
end
end





--14
go 
create procedure AddGoal
@LearnerID int,
@GoalID int
as 
begin
if exists (select * from learning_goal where goal_id = @GoalID)
begin
if not exists (select * from learning_goals where sid = @LearnerID and goal_id = @GoalID)
begin 
insert into learning_goals (sid, goal_id) values (@LearnerID, @GoalID)
end
end
else
begin 
insert into learning_goal (goal_id) values (@GoalID)
insert into learning_goals(sid, goal_id) values (@LearnerID, @GoalID)
end
end





--15
go 
create procedure CurrentPath 
@LearnerID int
as
begin
select * from learning_path lp where sid = @LearnerID
end



--16 
go 
create procedure QuestMembers
@LearnerID int
as 
begin 
select l.sid , l.fname,l.lname ,cq.deadline,q.title,q.difficulty_level,q.description ,q.quest_id from learners_collaboration lc inner join collaborative_quest cq on lc.quest_id = cq.quest_id 
inner join quest q on cq.quest_id = q.quest_id 
inner join learner l on lc.sid = l.sid where
@LearnerID in (select sid from learners_collaboration where quest_id = lc.quest_id)
and cq.deadline > getdate()
order by l.sid desc;
end



--17 
go 
create procedure QuestProgress
@LearnerID int
as
begin
select q.quest_id,lm.completion_status as skill_quest , lc.completetion_status as collaborative_quest from learner_mastery lm 
inner join quest q on lm.quest_id = q.quest_id inner join learners_collaboration lc on q.quest_id = lc.quest_id
where lm.sid = @LearnerID
end
exec QuestProgress 1;



--18 
GO
CREATE PROCEDURE GoalReminder
    @LearnerID INT,
    @GoalID INT
AS 
BEGIN
    -- Declare a variable to hold the notification ID
    DECLARE @NotificationID INT;

    -- Check if there are any learning goals with approaching deadlines for the specific learner
    IF EXISTS (
        SELECT 1
        FROM learning_goals l 
        INNER JOIN learning_goal lg ON lg.goal_id = l.goal_id 
        WHERE l.sid = @LearnerID AND lg.deadline > GETDATE() AND lg.goal_id = @GoalID
    )
    BEGIN
        -- Insert a new notification
        INSERT INTO notification (message, time_stamp, urgency) 
        VALUES ('Reminder: You have a learning goal deadline approaching.', GETDATE(), 'High');
        
        -- Capture the notification ID
        SET @NotificationID = SCOPE_IDENTITY();

        -- Insert into received_notification with the captured notification ID
        INSERT INTO recived_notification (sid, notification_id) 
        VALUES (@LearnerID, @NotificationID);
    END
END
GO




--19
go 
create procedure SkillProgressHistory
@LearnerID int,
@SkillName varchar(50)
as
begin
select * from skill_progression where sid = @LearnerID and skill_name = @SkillName
end




--20
go
create procedure AssessmentAnalysis
@LearnerID int
AS
begin
select * from taken_assessment ta
inner join assessment a on ta.assessment_id = a.assessment_id
where ta.sid = @LearnerID
end

exec AssessmentAnalysis 1;


--21
go
create procedure LeaderboardFilter
@LearnerID int
as
begin
select * from ranking inner join leader_board on ranking.board_id = leader_board.board_id
where @LearnerID = ranking.sid
order by rank desc
end









-- Instructor procedures

--1
go 
create procedure SkillLearners
@SkillName varchar(50)
as
begin
select l.sid,l.fname,l.lname,s.skill,l.country,l.gender ,l.culture from learner l
inner join skills s on l.sid = s.sid
where s.skill = @SkillName
end

drop procedure SkillLearners

exec SkillLearners 'C#';

--2
go
create procedure NewActivity
@CourseID int,
@ModuleID int,
@ActivityType varchar(50),
@Instructiondetails varchar(max),
@maxpoints int
as
   begin
   insert into learning_activity (cid, module_id, activity_type, instruction, max_points)
   values (@CourseID, @ModuleID, @ActivityType, @Instructiondetails, @maxpoints)
   end



exec NewActivity 1, 1, 'Quiz', 'Complete the quiz to test your knowledge.', 10;


select * from learning_activity


-- 3 
GO
CREATE PROCEDURE NewAchievement
    @LearnerID INT,
    @BadgeID INT,
    @description VARCHAR(max),
    @date_earned DATE,
    @type VARCHAR(50) = 'excellence'  -- Default value for type
AS
BEGIN
    -- Insert a new achievement record into the achievement table
    INSERT INTO acheivement (sid, badge_id, description, date_earned, type)
    VALUES (@LearnerID, @BadgeID, @description, @date_earned, @type);
    
   -- PRINT 'Achievement added successfully.';
END
GO

exec NewAchievement 1,3,'ddasdfasd','2202-12-23','exellence'
select * from acheivement


--4

go 
create procedure LearnerBadge 
@BadgeID INT 
as 
begin 
select distinct l.sid,l.fname , l.lname ,l.country ,l.gender from learner l
inner join acheivement a on l.sid = a.sid 
where a.badge_id =@BadgeID 
end


exec LearnerBadge 3;
select * from acheivement


--5 
select* from learning_path

GO
CREATE PROCEDURE NewPath
    @LearnerID INT,
    @ProfileID INT,
    @completion_status DECIMAL(3, 2) = 0.0,
    @custom_content VARCHAR(MAX),
    @adaptive_rules VARCHAR(MAX)
AS
BEGIN
    -- Insert into learning_path table
    INSERT INTO learning_path (sid, order_number, completion_status, custom_content)
    VALUES (@LearnerID, @ProfileID, @completion_status, @custom_content);
    
    -- Get the last inserted path_id (assuming learning_path has an IDENTITY column)
    DECLARE @LastPathID INT = SCOPE_IDENTITY();

    -- Insert adaptive rules into learning_path_adaptive_rules
    IF @adaptive_rules IS NOT NULL AND LEN(@adaptive_rules) > 0
    BEGIN
        INSERT INTO learning_path_rules (apadtive_rule, path_id)
        SELECT TRIM(value), @LastPathID
        FROM STRING_SPLIT(@adaptive_rules, ',')
        WHERE TRIM(value) <> '';  -- Exclude any empty values
    END
END
GO  -------needed now

exec NewPath 1, 1, 0.5, 'Custom content here', 'rule1, rule2, rule3';


--6 
 go 
 create procedure TakenCourses
 @LearnerID int
 as
    begin
    select c.cid, c.title, c.description, c.difficulty_level, c.learning_objective, c.credit_points,e.completion_date,e.enrollment_date
    from enrollment e 
    inner join course c on e.cid = c.cid
	where e.sid = @LearnerID
	end

    drop procedure TakenCourses

exec TakenCourses 1;



--7 
go 
create procedure CollaborativeQuest 
@dificulty_level varchar(50), 
@criteria varchar(50),
@description varchar(50),
@title varchar(50),
@Maxnumparticipants int,
@deadline datetime
as 
    begin
    insert into quest (difficulty_level, criteria, description, title)
    values (@dificulty_level, @criteria, @description, @title)
    insert into collaborative_quest (quest_id, max_participants, deadline)
    values (SCOPE_IDENTITY(), @Maxnumparticipants, @deadline)
	end


--7
GO
CREATE PROCEDURE CollaborativeQuest 
    @difficulty_level VARCHAR(50), 
    @criteria VARCHAR(50),
    @description VARCHAR(50),
    @title VARCHAR(50),
    @Maxnumparticipants INT,
    @deadline DATETIME
AS 
BEGIN
    -- Start a transaction
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Insert into the quest table
        INSERT INTO quest (difficulty_level, criteria, description, title)
        VALUES (@difficulty_level, @criteria, @description, @title);

        -- Get the last inserted quest_id
        DECLARE @LastQuestID INT = SCOPE_IDENTITY();

        -- Insert into the collaborative_quest table
        INSERT INTO collaborative_quest (quest_id, max_participants, deadline)
        VALUES (@LastQuestID, @Maxnumparticipants, @deadline);

        -- Commit the transaction
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Rollback the transaction in case of an error
        ROLLBACK TRANSACTION;

        -- Optionally log the error or handle it as needed
        DECLARE @ErrorMessage NVARCHAR(4000);
        SET @ErrorMessage = ERROR_MESSAGE();
        PRINT 'An error occurred: ' + @ErrorMessage;
    END CATCH
END
GO

exec CollaborativeQuest 'Intermediate', 'Complete all tasks', 'Collaborative quest description', 'Collaborative Quest Title', 5, '2023-12-31 23:59:59';
select * from quest
select * from collaborative_quest




--8
go 
create procedure DeadlineUpdate
@QuestID int,
@deadline datetime
as
begin
update collaborative_quest
set deadline = @deadline
where quest_id = @QuestID
end

exec DeadlineUpdate 1, '2029-12-31 23:59:59';

select * from collaborative_quest



--9
go 
create procedure GradeUpdate
@LearnerID int,
@AssessmentID int,
@score int
as
begin
update taken_assessment
set scored_points = @score
where sid = @LearnerID and assessment_id = @AssessmentID
print 'Grade updated successfully'
end

--10
go 
create procedure AssessmentNot
@NotificationID int,
@timestamp datetime,
@message varchar(max),
@urgencylevel varchar(50),
@LearnerID int
as
begin

(select @NotificationID= max (notification_id)+1 from 
notification) 
set  identity_insert notification on
insert into notification (notification_id,message, time_stamp, urgency)
values (@NotificationID,@message, GETDATE(), @urgencylevel)
insert into recived_notification (sid, notification_id)
values (@LearnerID, SCOPE_IDENTITY())
print 'Notification sent successfully'
set identity_insert notification off
end
drop procedure AssessmentNot



--11
go 
create procedure NewGoal
@GoalID int,
@status varchar(50),
@deadline datetime,
@description varchar(50)
as
begin
if not exists ( select 1 from learning_goal where goal_id = @GoalID)
begin
insert into learning_goal (goal_id, status, deadline, description)
values (@GoalID, @status, @deadline, @description)
end
else
begin
print 'Goal already exists'
end
end

exec NewGoal 19, 'incomplete', '2023-12-31', 'Complete Python course';

select * from learning_goal



--12
go 
create procedure LearnersCoutrses
@CourseID int,
@InstructorID int
as
begin
select l.sid, l.fname, l.lname, l.country , c.title, c.description, c.difficulty_level, c.learning_objective, c.credit_points
from learner l
inner join enrollment e on l.sid = e.sid
inner join course c on e.cid = c.cid
inner join teaches t on c.cid = t.cid
where c.cid = @CourseID and t.instructor_id = @InstructorID
end




--13
go 
create procedure LastActive 
@ForumID int, 
@lastactive datetime output
as 
begin
select top 1 @lastactive = l.last_activity  from disscussion_forum l  where l.forum_id = @ForumID
order by time_stamp desc
end


--14
go 
create procedure CommonEmotiobnalState
@state varchar(50) output
as
begin 
    select top 1 @state =p.emotional_state from personalization_profile  p
    group by p.emotional_state
    order by count(*) desc
end




--15
go 
create procedure ModuleDifficulty 
@CourseID int
as 
begin 
select * from modules where cid = @CourseID
order by difficulty_level desc
end

exec ModuleDifficulty 1;




--16
GO
CREATE PROCEDURE Profeciencylevel
    @LearnerID INT,
    @highestSkillNames VARCHAR(MAX) OUTPUT
AS 
BEGIN
    -- Initialize the output variable
    SET @highestSkillNames = '';

    DECLARE @highestProficiencyLevel INT;

    -- Determine the highest proficiency level for the learner
    SELECT @highestProficiencyLevel = MAX(
        CASE 
            WHEN sp.proficiency_level = 'Beginner' THEN 1
            WHEN sp.proficiency_level = 'Intermediate' THEN 2
            WHEN sp.proficiency_level = 'Advanced' THEN 3
            ELSE 0
        END)
    FROM 
        skill_progression sp
    WHERE 
        sp.sid = @LearnerID;

    -- Select all skill names that match the highest proficiency level
    SELECT @highestSkillNames = STRING_AGG(sp.skill_name, ', ')  -- Concatenate skill names
    FROM 
        skill_progression sp
    WHERE 
        sp.sid = @LearnerID
        AND (
            CASE 
                WHEN sp.proficiency_level = 'Beginner' THEN 1
                WHEN sp.proficiency_level = 'Intermediate' THEN 2
                WHEN sp.proficiency_level = 'Advanced' THEN 3
                ELSE 0
            END
        ) = @highestProficiencyLevel;

    -- If no skills are found, you can set the output to a default message
    IF @highestSkillNames = ''
    BEGIN
        SET @highestSkillNames = 'No skills found at the highest proficiency level';
    END
END;
GO



--17
go 
create procedure ProfeciencyUpdate
@Skill varchar(50),
@LearnerID int,
@Level varchar(50)
as
begin
if exists (select * from skill_progression where sid = @LearnerID and skill_name = @Skill)
begin
update skill_progression
set proficiency_level = @Level
where sid = @LearnerID and skill_name = @Skill
end
end

exec ProfeciencyUpdate 'Python', 5, 'Advanced';


--19 
GO
CREATE PROCEDURE LeastBadge
    @LearnerID INT OUTPUT
AS
BEGIN
    -- Select the learner ID with the least number of badges
    SELECT TOP 1 @LearnerID = a.sid
    FROM acheivement a
    GROUP BY a.sid
    ORDER BY COUNT(*) ASC;  -- Order by the count of badges in ascending order
END;
GO

--18
GO
CREATE PROCEDURE LeastBadge
@LearnerID INT OUTPUT
AS
BEGIN
    -- Declare a variable to hold the minimum badge count
    DECLARE @MinBadgeCount INT;

    -- Find the minimum badge count
    SELECT @MinBadgeCount = MIN(BadgeCount)
    FROM (
        SELECT COUNT(*) AS BadgeCount
        FROM achievement
        GROUP BY sid
    ) AS BadgeCounts;

    -- Select all learners with the minimum badge count
    SELECT a.sid AS LearnerID
    FROM achievement a
    GROUP BY a.sid
    HAVING COUNT(*) = @MinBadgeCount;
END;
GO
drop procedure LeastBadge
declare @LearnerID int
exec LeastBadge @LearnerID output
select @LearnerID as LeastBadge;

select * from acheivement

--19
GO
CREATE PROCEDURE PreferedType
    @types VARCHAR(MAX) OUTPUT  -- Output parameter to return the most preferred types
AS
BEGIN
    -- Variable to hold the maximum count of preferences
    DECLARE @MaxCount INT;

    -- Find the maximum count of preferences
    SELECT @MaxCount = MAX(PreferenceCount)
    FROM (
        SELECT COUNT(*) AS PreferenceCount
        FROM learning_preferences
        GROUP BY preference
    ) AS PreferenceCounts;

    -- Select all preferred types that have the maximum count
    SELECT @types = STRING_AGG(pre, ', ')  -- Concatenate preferred types
    FROM (
        SELECT preference AS pre
        FROM learning_preferences
        GROUP BY preference
        HAVING COUNT(*) = @MaxCount
    ) AS MostPreferredTypes;
END;
GO

--20 
GO
CREATE PROCEDURE AssessmentAnalytics
    @CourseID INT,
    @ModuleID INT
AS
BEGIN
    -- Select the average assessment scores for each learner
    SELECT 
        l.sid AS LearnerID,
        l.fname AS FirstName,
        l.lname AS LastName,
        AVG(ta.scored_points) AS AverageScore
    FROM 
        learner l
    INNER JOIN 
        taken_assessment ta ON l.sid = ta.sid
    INNER JOIN 
        assessment a ON ta.assessment_id = a.assessment_id
    WHERE 
        a.cid = @CourseID AND 
        a.module_id = @ModuleID
    GROUP BY 
        l.sid, l.fname, l.lname
    ORDER BY 
        AverageScore DESC;  -- Order by average score in descending order
END;

exec AssessmentAnalytics 1, 1;


--21
go
create procedure EmotionalTrendAnalysisInstructor 
@CourseID int,
@ModuleID int,
@TimePeriod datetime,
@InstructorID int
as 
begin
select sid,emotional_state from emotional_feedback ef
inner join learning_activity la on ef.activity_id=la.activity_id
inner join teaches t on la.cid = t.cid
where la.cid = @CourseID and la.module_id = @ModuleID and ef.time_stamp >= @TimePeriod and t.instructor_id = @InstructorID
group by 
ef.sid,ef.emotional_state
order by 
ef.sid
end
exec EmotionalTrendAnalysisInstructor 1, 1, '2023-10-15 10:00:00', 2;

go
create procedure SignInUser
	 @Username VARCHAR(50),
    @Password VARCHAR(50),
    @Email VARCHAR(50),
    @Role VARCHAR(50),
    @FirstName VARCHAR(50) = NULL,  -- Only for learners
    @LastName VARCHAR(50) = NULL,    -- Only for learners
    @Gender BIT = NULL,               -- Only for learners
    @BirthDate DATE = NULL,           -- Only for learners
    @Country VARCHAR(50) = NULL ,-- Only for learners
    @Expertise VARCHAR(50) = NULL,   -- Only for instructors
    @Qualification VARCHAR(50) = NULL -- Only for instructors
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if the email already exists
    IF EXISTS (SELECT 1 FROM users WHERE email = @Email)
    BEGIN
        RAISERROR('Email already exists.', 16, 1);
        RETURN;
    END

    -- Check if the username already exists
    IF EXISTS (SELECT 1 FROM users WHERE username = @Username)
    BEGIN
        RAISERROR('Username already exists.', 16, 1);
        RETURN;
    END

    -- Insert into the users table
    INSERT INTO users (username, password, email, role)
    VALUES (@Username, @Password, @Email, @Role);

    DECLARE @UserId INT = SCOPE_IDENTITY(); -- Get the ID of the newly inserted user

    -- Insert into the appropriate role-specific table
    IF @Role = 'admin'
    BEGIN
        INSERT INTO admin (aid) VALUES (@UserId);
    END
    ELSE IF @Role = 'instructor'
    BEGIN
        INSERT INTO instructor (instructor_id, name, latest_qualification, expertise_area)
        VALUES (@UserId, @Username, @Qualification, @Expertise); -- Adjust as necessary
    END
    ELSE IF @Role = 'learner'
    BEGIN
        INSERT INTO learner (sid, fname, lname, gender, birth_date, country)
        VALUES (@UserId, @FirstName, @LastName, @Gender, @BirthDate, @Country);
    END
    ELSE
    BEGIN
        RAISERROR('Invalid role specified.', 16, 1);
        RETURN;
    END
END;


go 
create procedure availableCourses
as
begin
select * from course
end


    go
CREATE PROCEDURE UserLogin
    @Username VARCHAR(50),
    @Password VARCHAR(50),
    @status BIT OUTPUT,
    @Type VARCHAR(50) OUTPUT  -- Output parameter to store the user role (admin, instructor, learner)
AS
BEGIN
    SET NOCOUNT ON;

    -- Attempt to retrieve the user based on username and password
    SELECT 
        @Type = role,
        @status = 1
    FROM 
        users
    WHERE 
        username = @Username AND password = @Password;

    -- If no user is found, set status to 0
    IF @Type IS NULL
    BEGIN
        SET @status = 0;
    END
END;



/*
go
CREATE TRIGGER trg_CheckDuplicateUser  
ON users
INSTEAD OF INSERT
AS
BEGIN
    -- Check for duplicates in the username
    IF EXISTS (SELECT 1 FROM users WHERE username IN (SELECT username FROM inserted))
    BEGIN
        RAISERROR('Username already exists.', 16, 1);
        ROLLBACK TRANSACTION; -- Rollback the transaction
        RETURN; -- Exit the trigger
    END

    -- Check for duplicates in the email
    IF EXISTS (SELECT 1 FROM users WHERE email IN (SELECT email FROM inserted))
    BEGIN
        RAISERROR('Email already exists.', 16, 1);
        ROLLBACK TRANSACTION; -- Rollback the transaction
        RETURN; -- Exit the trigger
    END

    -- Insert into the appropriate table based on the role
    INSERT INTO learner (username, password, email, role)
    SELECT username, password, email, role 
    FROM inserted 
    WHERE role = 'learner';

    INSERT INTO admin (username, password, email, role)
    SELECT username, password, email, role 
    FROM inserted 
    WHERE role = 'admin';

    INSERT INTO instructor (username, password, email, role)
    SELECT username, password, email, role 
    FROM inserted 
    WHERE role = 'instructor';
END;
*/

drop procedure SignInUser
go 
create PROCEDURE SignInUser
    @Username VARCHAR(50),
    @Password VARCHAR(50),
    @Email VARCHAR(50),
    @Role VARCHAR(50),
    @FirstName VARCHAR(50) = NULL,   -- For learners
    @LastName VARCHAR(50) = NULL,    -- For learners
    @Gender BIT = NULL,              -- For learners
    @BirthDate DATE = NULL,          -- For learners
    @Country VARCHAR(50) = NULL,     -- For learners
    @Expertise VARCHAR(50) = NULL,   -- For instructors
    @Qualification VARCHAR(50) = NULL, -- For instructors
    @Name VARCHAR(50) = NULL         -- For instructors
AS
BEGIN
    SET NOCOUNT ON;

    -- Insert into Users table
    INSERT INTO Users (Username, Password, Email, Role)
    VALUES (@Username, @Password, @Email, @Role);

    -- Get the newly inserted User ID
    DECLARE @UserId INT;
    SET @UserId = SCOPE_IDENTITY();

    -- Insert into specific tables based on Role
    IF @Role = 'Learner'
    BEGIN
        INSERT INTO Learner (Sid, Fname, Lname, Gender, Birth_Date, Country)
        VALUES (@UserId, @FirstName, @LastName, @Gender, @BirthDate, @Country);
    END
    ELSE IF @Role = 'Instructor'
    BEGIN
        INSERT INTO Instructor (Instructor_Id, Name, Expertise_Area, Latest_Qualification)
        VALUES (@UserId, @Name, @Expertise, @Qualification);
    END
    ELSE IF @Role = 'Admin'
    BEGIN
        INSERT INTO Admin (Aid)
        VALUES (@UserId);
    END
END;

go
create proc sp_RegisterUser
@Username VARCHAR(50),
    @Password VARCHAR(50),
    @Email VARCHAR(50),
    @Role VARCHAR(50),
    @FirstName VARCHAR(50) = NULL,
    @LastName VARCHAR(50) = NULL,
    @Gender BIT = NULL,
    @BirthDate DATE = NULL,
    @Country VARCHAR(50) = NULL,
    @Name VARCHAR(50) = NULL,
    @Expertise VARCHAR(50) = NULL,
    @Qualification VARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if Username or Email already exists
    IF EXISTS (SELECT 1 FROM users WHERE username = @Username)
    BEGIN
        RAISERROR ('Username already exists.', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM users WHERE email = @Email)
    BEGIN
        RAISERROR ('Email already exists.', 16, 1);
        RETURN;
    END

    -- Insert into Users table
    INSERT INTO users (username, password, email, role)
    VALUES (@Username, @Password, @Email, @Role);

    -- Get the generated User ID
    DECLARE @UserId INT = SCOPE_IDENTITY();

    -- Insert into Role-specific tables
    IF @Role = 'Learner'
    BEGIN
        INSERT INTO learner (sid, fname, lname, gender, birth_date, country)
        VALUES (@UserId, @FirstName, @LastName, @Gender, @BirthDate, @Country);
    END
    ELSE IF @Role = 'Instructor'
    BEGIN
        INSERT INTO instructor (instructor_id, name, expertise_area, latest_qualification)
        VALUES (@UserId, @Name, @Expertise, @Qualification);
    END
    ELSE IF @Role = 'Admin'
    BEGIN
        INSERT INTO admin (aid)
        VALUES (@UserId);
    END
END
