-- 创建数据库
CREATE DATABASE SurveyDB;
GO

USE SurveyDB;
GO

-- 用户表
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100),
    FullName NVARCHAR(100),
    Role NVARCHAR(20),
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    LastLoginDate DATETIME
);

-- 问卷表
CREATE TABLE Surveys (
    SurveyID INT PRIMARY KEY IDENTITY(1,1),
    SurveyTitle NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    CreatorID INT NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    StartDate DATETIME,
    EndDate DATETIME,
    Status NVARCHAR(20),
    AllowAnonymous BIT DEFAULT 1,
    FOREIGN KEY (CreatorID) REFERENCES Users(UserID)
);

-- 问题表
CREATE TABLE Questions (
    QuestionID INT PRIMARY KEY IDENTITY(1,1),
    SurveyID INT NOT NULL,
    QuestionText NVARCHAR(MAX) NOT NULL,
    QuestionType NVARCHAR(20),
    OrderNum INT,
    IsRequired BIT DEFAULT 1,
    FOREIGN KEY (SurveyID) REFERENCES Surveys(SurveyID) ON DELETE CASCADE
);

-- 选项表
CREATE TABLE Options (
    OptionID INT PRIMARY KEY IDENTITY(1,1),
    QuestionID INT NOT NULL,
    OptionText NVARCHAR(255) NOT NULL,
    OrderNum INT,
    FOREIGN KEY (QuestionID) REFERENCES Questions(QuestionID) ON DELETE CASCADE
);

-- 问卷回复表
CREATE TABLE Responses (
    ResponseID INT PRIMARY KEY IDENTITY(1,1),
    SurveyID INT NOT NULL,
    RespondentID INT,
    SubmittedDate DATETIME DEFAULT GETDATE(),
    IPAddress NVARCHAR(50),
    FOREIGN KEY (SurveyID) REFERENCES Surveys(SurveyID),
    FOREIGN KEY (RespondentID) REFERENCES Users(UserID)
);

-- 答案表
CREATE TABLE Answers (
    AnswerID INT PRIMARY KEY IDENTITY(1,1),
    ResponseID INT NOT NULL,
    QuestionID INT NOT NULL,
    AnswerText NVARCHAR(MAX),
    SelectedOptionID INT,
    FOREIGN KEY (ResponseID) REFERENCES Responses(ResponseID) ON DELETE CASCADE,
    FOREIGN KEY (QuestionID) REFERENCES Questions(QuestionID),
    FOREIGN KEY (SelectedOptionID) REFERENCES Options(OptionID)
);

-- 创建索引
CREATE INDEX idx_surveys_creator ON Surveys(CreatorID);
CREATE INDEX idx_questions_survey ON Questions(SurveyID);
CREATE INDEX idx_responses_survey ON Responses(SurveyID);
CREATE INDEX idx_answers_response ON Answers(ResponseID);
CREATE INDEX idx_users_username ON Users(Username);

-- 插入测试数据
INSERT INTO Users (Username, Password, Email, FullName, Role, IsActive)
VALUES 
    ('admin', '123456', 'admin@survey.com', '管理员', 'Admin', 1),
    ('user1', '123456', 'user1@survey.com', '用户1', 'User', 1),
    ('user2', '123456', 'user2@survey.com', '用户2', 'User', 1);

GO