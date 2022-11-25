create database Capstone
use Capstone
create table Student(
student_id int identity primary key,
student_name varchar(max),
student_email varchar(250) NOT NULL ,
student_department varchar(max),
student_password varchar(max),
course_id int,
foreign key(course_id) references StudentCourse(student_id),
constraint unique_email unique(student_email)
);


create table Admins(
admin_id int identity primary key,
admin_name varchar(250)not null,
admin_password varchar(250)
constraint unique_admin unique(admin_name)
);

create table StudentCourse(
student_id int identity primary key,
course1 varchar(max),
course2 varchar(max),
course3 varchar(max),
course4 varchar(max),
course5 varchar(max)

)

insert into StudentCourse(course1,course2,course3,course4,course5) values('DataBase Mgt System','Operating System','Artificial Intelligence','Applied Algebra','Networks')
insert into StudentCourse(course1,course2,course3,course4,course5) values('	Basic Electrical Engineering','Digital Logic','Microprocessor','Electric Machine Design','Signal Analysis')
insert into StudentCourse(course1,course2,course3,course4,course5) values('Manufacturing And Production Processes','Heat Transfer','Fluid Machines','Mechanics of Solids','Strength of Materials')

insert into Student (student_name,student_email,student_department,student_password,course_id) values('Manish Jaiswal','manishjaiswal@gmail.com','CSE','capstone123@',1);


insert into Student (student_name,student_email,student_department,student_password,course_id) values('Mukesh Lodha','mukeshlodha@gmail.com','ME','mukesh123',1);

select * from Student

insert into admins (admin_name,admin_password) values('admin1','adminpassword1')
insert into admins (admin_name,admin_password) values('admin2','adminpassword2')
select * from admins
select * from StudentCourse 


create or alter procedure admin_verify(@name varchar(250),@password varchar(250))
as
Select count (*) from admins where admin_name=@name and admin_password=@password

create or alter procedure stu_details as
select * from Student

create or alter procedure student_signup(@sname varchar(250),@semail varchar(250),@sdepartment varchar(250),@spassword varchar(250),@scourse int)
as
insert into Student (student_name,student_email,student_department,student_password,course_id) values(@sname,@semail,@sdepartment,@spassword,@scourse)

create or alter procedure student_verify(@semail varchar(250),@spassword varchar(250))
as
Select count (*) from Student where student_email=@semail and student_password=@spassword


create or alter procedure update_student(@sid int,@sname varchar(250),@semail varchar(50),@sdepartment varchar(250),@spassword varchar(250),@scourseid int)
as
update Student set student_name=@sname,student_email=@semail,student_department=@sdepartment, student_password=@spassword ,course_id=@scourseid where student_id=@sid

create or alter procedure update_course(@course_id int,@course1 varchar(250),@course2 varchar(50),@course3 varchar(250),@course4 varchar(250),@course5 varchar(250))
as
update StudentCourse set course1=@course1, @course2=@course2,course3=@course3,@course4=@course4,course5=@course5 where student_id=@course_id

exec update_course 3,'Manufacturing and Production Process','Heat Transfer','fluid mechanics' ,'Mechanics Of Solids','Strength Of Material'
create or alter procedure student_fetch(@semail varchar(250),@spassword varchar(250))
as
Select student_email,student_password from Student where student_email=@semail and student_password=@spassword

create or alter procedure student_delete(@sid int)
as
delete from Student where student_id=@sid

select course1,course2,course3,course4,course5 from StudentCourse c,Student s where s.course_id=c.student_id