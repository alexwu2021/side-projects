drop database if exists zed;

create database zed;
commit;

use zed;

SET FOREIGN_KEY_CHECKS = 0;

-- static tables --
DROP TABLE IF EXISTS task_types;
CREATE TABLE task_types (
                               id int NOT NULL AUTO_INCREMENT,
                               name nvarchar(50) NOT NULL,
                               description nvarchar(200) NOT NULL,
                               PRIMARY KEY ( id )
) ENGINE=InnoDB DEFAULT CHARSET=utf32;

insert into task_types (name, description) values ('health', 'health related');
insert into task_types (name, description) values ('work', 'work related');
insert into task_types (name, description) values ('learning', 'learning related');
insert into task_types (name, description) values ('other', 'all the other stuff');

DROP TABLE IF EXISTS task_status;
CREATE TABLE task_status (
                            id int NOT NULL AUTO_INCREMENT,
                            name nvarchar(50) NOT NULL,
                            description nvarchar(200) NOT NULL,
                            PRIMARY KEY ( id )
) ENGINE=InnoDB DEFAULT CHARSET=utf32;
insert into task_status (name, description) values ('created', 'just created and no action taken');
insert into task_status (name, description) values ('pending but not reminded', 'created, within 60 minutes to the task start time');
insert into task_status (name, description) values ('reminding but not finished', 'start reminding but the end time has not been reached yet');
insert into task_status (name, description) values ('in session', 'in session and all the reminders have been executed');
insert into task_status (name, description) values ('finished', 'finished');



DROP TABLE IF EXISTS reminder_types;
CREATE TABLE reminder_types (
                                id int NOT NULL AUTO_INCREMENT,
                                name nvarchar(50) NOT NULL,
                                description nvarchar(200) NOT NULL,
                                PRIMARY KEY ( id )
) ENGINE=InnoDB DEFAULT CHARSET=utf32;
insert into reminder_types (name, description)
values ('flashing on UI', 'for the control that is tied to a task, flash it');
insert into reminder_types (name, description)
values ('ballooning a message', 'balloon an alert message from the right corner of a computer screen');

DROP TABLE IF EXISTS task_priority;
CREATE TABLE task_priority (
                        id int NOT NULL AUTO_INCREMENT,
                        name nvarchar(50) NOT NULL,
                        description nvarchar(200) NOT NULL,
                        PRIMARY KEY ( id )
) ENGINE=InnoDB DEFAULT CHARSET=utf32;
insert into task_priority (name, description) values ('high', 'high');
insert into task_priority (name, description) values ('medium', 'medium');
insert into task_priority (name, description) values ('low', 'low');



-- the main tables --
DROP TABLE IF EXISTS tasks;
CREATE TABLE tasks (
                       id int NOT NULL AUTO_INCREMENT,
                       name nvarchar(50) NOT NULL,
                       description nvarchar(200) NOT NULL,
                       task_priority_id int not null default 2,
                       task_type_id int not null,
                       start_time TIMESTAMP not null,
                       duration_in_minutes int not null default (30),
                       need_reminder boolean not null,
                       PRIMARY KEY ( id ),
                       CONSTRAINT uk_tasks UNIQUE KEY (name, start_time, duration_in_minutes),
                       CONSTRAINT tasks_task_types FOREIGN KEY (task_type_id)
                           REFERENCES task_types (id),
                       CONSTRAINT tasks_task_priority_types FOREIGN KEY (task_priority_id)
                           REFERENCES task_priority (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf32;
insert into tasks(name, description, task_priority_id, task_type_id, start_time, need_reminder)
values ('annual health check', 'visit family doctor to do the annual health check 2021', 3, 1, now(), true);

insert into tasks(name, description, task_priority_id, task_type_id, start_time, need_reminder)
values ('covid-19 check', 'covid checking needed for annual health check office visit', 2, 1, now(), true);

insert into tasks(name, description, task_priority_id, task_type_id, start_time,need_reminder)
values ('get project x done', 'complete the development of project x', 1, 2, now(), true);

insert into tasks(name, description, task_priority_id, task_type_id, start_time, need_reminder)
values ('read novel y', 'read novel y', 3, 3, now(), false);


DROP TABLE IF EXISTS task_dependency;
CREATE TABLE task_dependency (
                       id int NOT NULL AUTO_INCREMENT,
                       depending_task_id int not null,
                       task_id int not null,
                       PRIMARY KEY ( id ),
                       CONSTRAINT uk_task_dependency UNIQUE KEY (depending_task_id, task_id),
                       CONSTRAINT task_dependency_tasks_depending_task_id FOREIGN KEY (depending_task_id)
                           REFERENCES tasks (id),
                       CONSTRAINT task_dependency_tasks_task_id FOREIGN KEY (task_id)
                           REFERENCES tasks (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf32;
insert into task_dependency (depending_task_id, task_id) values (1, 2);

DROP TABLE IF EXISTS reminders;
CREATE TABLE reminders (
                                 id int NOT NULL AUTO_INCREMENT,
                                 name nvarchar(50),
                                 description nvarchar(150),
                                 task_id int not null,
                                 reminder_type_id int not null default 1,
                                 first_reminder_lead_time_in_minute int not null default 15,
                                 number_of_retries int not null default 3,
                                 frequency_in_minute int not null default 5,
                                 CONSTRAINT uk_reminders UNIQUE KEY (task_id, reminder_type_id),
                                 PRIMARY KEY ( id ),
                                 CONSTRAINT reminders_tasks_task_id FOREIGN KEY (task_id)
                                     REFERENCES tasks (id),
                                 CONSTRAINT reminders_type_id FOREIGN KEY (reminder_type_id)
                                     REFERENCES reminder_types (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf32;
insert into reminders (name, description, task_id, reminder_type_id,
                       first_reminder_lead_time_in_minute, number_of_retries, frequency_in_minute)
values ('annual heal health check reminder', 'annual heal health check reminder', 1, 1, 15, 3, 5);

SET FOREIGN_KEY_CHECKS = 1;