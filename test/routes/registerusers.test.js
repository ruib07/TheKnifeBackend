const request = require('supertest');

const app = require('../../src/app');

const route = '/registerusers';

test('Test #45 - Listar Utilizadores', () => {
  return request(app).get(route)
    .then((res) => {
      expect(res.status).toBe(200);
    });
});

test('Test #46 - Listar um utilizador por ID', () => {
  return app.db('registerusers')
    .insert({
      username: 'goncalosousa',
      email: 'goncalosousa@gmail.com',
      password: 'goncalo123',
    }, ['id'])
    .then((userRes) => request(app).get(`${route}/${userRes[0].id}`))
    .then((res) => {
      expect(res.status).toBe(200);
      expect(res.body.username).toBe('goncalosousa');
    });
});

test('Test #47 - Atualizar dados de um utilizador', () => {
  return app.db('registerusers')
    .insert({
      username: 'goncalosousa',
      email: 'goncalosousa@gmail.com',
      password: 'goncalo123',
    }, ['id'])
    .then((userRes) => request(app).put(`${route}/${userRes[0].id}`)
      .send({
        username: 'GoncaloCoutinho',
        email: 'goncalocoutinho@gmail.com',
        password: 'goncalo987',
      }))
    .then((res) => {
      expect(res.status).toBe(200);
      expect(res.body).not.toHaveProperty('password');
    });
});

test('Test #48 - Inserir um registo de um utilizador', async () => {
  const registrationResponse = await request(app).post(route)
    .send({
      username: 'goncalosousa',
      email: 'goncalosousa@gmail.com',
      password: 'goncalo123',
    });

  expect(registrationResponse.status).toBe(201);
  expect(registrationResponse.body).not.toHaveProperty('password');
});

test('Test #48.1 - Guardar password encriptada', async () => {
  const res = await request(app).post(route)
    .send({
      username: 'goncalosousa',
      email: 'goncalosousa@gmail.com',
      password: 'goncalo123',
    });

  expect(res.status).toBe(201);

  const { id } = res.body;
  const registerUsersDB = await app.services.registeruser.find({ id });
  expect(registerUsersDB.password).not.toBeUndefined();
  expect(registerUsersDB.password).not.toBe('goncalo123');
});

test('Test #49 - Inserir um utilizador sem username', () => {
  return request(app).post(route)
    .send({
      email: 'goncalosousa@gmail.com',
      password: 'goncalo123',
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Username é um atributo obrigatório!');
    });
});

test('Test #50 - Inserir um utilizador sem email', () => {
  return request(app).post(route)
    .send({
      username: 'goncalosousa',
      user_password: 'goncalo123',
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Email é um atributo obrigatório!');
    });
});

test('Test #51 - Inserir um utilizador sem password', () => {
  return request(app).post(route)
    .send({
      username: 'goncalosousa',
      email: 'goncalosousa@gmail.com',
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Password é um atributo obrigatório!');
    });
});

test('Test #53 - Inserir e confirmar a Palavra Passe', async () => {
  const registrationResponse = await request(app).post(route)
    .send({
      username: 'goncalosousa',
      email: 'goncalosousa@gmail.com',
      password: 'goncalo123',
    });

  expect(registrationResponse.status).toBe(201);

  const updatePasswordResponse = await request(app).put(`${route}/${registrationResponse.body.id}/updatepassword`)
    .send({
      newPassword: 'goncalo321',
      confirmNewPassword: 'goncalo321',
    });

  expect(updatePasswordResponse.status).toBe(200);
  expect(updatePasswordResponse.body.message).toBe('Palavra Passe atualizada com sucesso!');
});

test('Test #54 - Inserir Palavra Passes diferentes', async () => {
  const registrationResponse = await request(app).post(route)
    .send({
      username: 'goncalosousa',
      email: 'goncalosousa@gmail.com',
      password: 'goncalo123',
    });

  expect(registrationResponse.status).toBe(201);

  const updatePasswordResponse = await request(app).put(`${route}/${registrationResponse.body.id}/updatepassword`)
    .send({
      newPassword: 'goncalo321',
      confirmNewPassword: 'goncalo231',
    });

  expect(updatePasswordResponse.status).toBe(400);
  expect(updatePasswordResponse.body.error).toBe('A Palavra Passe deve ser igual nos dois campos!');
});
