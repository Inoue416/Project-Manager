INSERT INTO users(name, user_kind) VALUES (
    '井上優也', 0);

INSERT INTO users(name, user_kind) VALUES (
    '今村環紀', 0);

INSERT INTO users(name, user_kind) VALUES (
    '藤優駿', 0);

INSERT INTO users(name, user_kind) VALUES (
    '田中こうた', 0);

INSERT INTO record(title, content, content_kind) VALUES (
    '今日の進捗 - 2023/11/05',
    ' 今日の進捗としてはまずDocker compose upでビルドしてコンテナを起動する際に、\
    SQLスクリプトを起動する方法を学んだ。\n',
    0);

INSERT INTO record_has(user_id, record_id) VALUES(
    1, 1);

INSERT INTO office(group_name, owner_id) VALUES(
    'Test Group', 1);

INSERT INTO office_has(user_id, office_id) VALUES(
    1, 1);

INSERT INTO office_has(user_id, office_id) VALUES(
    2, 1);

INSERT INTO office_has(user_id, office_id) VALUES(
    3, 1);

INSERT INTO office_has(user_id, office_id) VALUES(
    4, 1);
