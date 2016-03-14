SELECT * FROM AspNetRoles;

SELECT * FROM AspNetUsers;

SELECT * FROM AspNetUserClaims;

SELECT * FROM AspNetUserLogins;

SELECT * FROM AspNetUserRoles;

INSERT INTO AspNetRoles(Id,Name)
		VALUES(1, 'admin'),
			  (2, 'user');

INSERT INTO AspNetUserRoles(UserId, RoleId)
					VALUES('00ef1129-116a-46ed-a1cc-d4c85074c08f', 1),
						  ('cac0d057-9448-4307-bf84-b6c45d5baafa', 1),
						  ('e0d4d5ee-d201-43f2-b1a8-dd0fa530041f', 2),
						  ('de076117-ddcf-402c-8e27-30a1532a8d4d', 2);