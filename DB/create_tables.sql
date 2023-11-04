CREATE TABLE IF NOT EXISTS FIT_DB.users (
    id PRIMARY KEY NOT NULL,
    name varchar(50) NOT NULL,
    office_id int NULL,
    user_kind int NOT NULL,
    created_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS FIT_DB.record (
    id PRIMARY KEY NOT NULL,
    title varchar(100) NOT NULL,
    content varchar(10000) NOT NULL,
    content_kind int NOT NULL DEFAULT 0,
    created_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS FIT_DB.record_has (
    id PRIMARY KEY NOT NULL,
    foreign key(user_id) references FIT_DB.users(id) NOT NULL,
    foreign key(record_id) references FIT_DB.record(id) NOT NULL,
    created_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP
)

CREATE TABLE IF NOT EXISTS FIT_DB.office (
    id PRIMARY KEY NOT NULL,
    group_name varchar(100) NOT NULL,
    owner_id int NOT NULL,
    created_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS FIT_DB.office_has (
    id PRIMARY KEY NOT NULL,
    foreign key(user_id) references FIT_DB.users(id) NOT NULL,
    foreign key(office_id) references FIT_DB.office(id) NOT NULL,
    created_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP
);