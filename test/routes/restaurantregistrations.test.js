const request = require('supertest');

const app = require('../../src/app');

const route = '/restaurantregistrations';

test('Test #1 - Listar todos os restaurantes registados', () => {
  return request(app).get(route)
    .then((res) => {
      expect(res.status).toBe(200);
    });
});

test('Test #2 - Listar um restaurante por ID', () => {
  return app.db('restaurantregistrations')
    .insert({
      flname: 'Rui Barreto',
      phone: 912345678,
      email: 'ruibarreto@gmail.com',
      password: '12345',
      name: 'La Gusto Italiano',
      category: 'Comida Italiana',
      desc: 'Restaurante de comida italiana situado em Braga',
      rphone: 253456789,
      location: 'Rua Gonçalo Sousa 285',
      image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
      numberoftables: 10,
      capacity: 200,
      openingdays: 'Aberto de segunda a sábado',
      openinghours: '10:30',
      closinghours: '23:00',
    }, ['id'])
    .then((restaurantRes) => request(app).get(`${route}/${restaurantRes[0].id}`))
    .then((res) => {
      expect(res.status).toBe(200);
      expect(res.body.flname).toBe('Rui Barreto');
    });
});

test('Test #3 - Inserir registo de restaurantes', () => {
  return request(app).post(route)
    .send({
      flname: 'Rui Barreto',
      phone: 912345678,
      email: 'ruibarreto@gmail.com',
      password: '12345',
      name: 'La Gusto Italiano',
      category: 'Comida Italiana',
      desc: 'Restaurante de comida italiana situado em Braga',
      rphone: 253456789,
      location: 'Rua Gonçalo Sousa 285',
      image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
      numberoftables: 10,
      capacity: 200,
      openingdays: 'Aberto de segunda a sábado',
      openinghours: '10:30',
      closinghours: '23:00',
    })
    .then((res) => {
      expect(res.status).toBe(201);
      expect(res.body.name).toBe('La Gusto Italiano');
      expect(res.body).not.toHaveProperty('password');
    });
});

test('Test #3.1 - Guardar password encriptada', async () => {
  const res = await request(app).post(route)
    .send({
      flname: 'Rui Barreto',
      phone: 912345678,
      email: 'ruibarreto@gmail.com',
      password: '12345',
      name: 'La Gusto Italiano',
      category: 'Comida Italiana',
      desc: 'Restaurante de comida italiana situado em Braga',
      rphone: 253456789,
      location: 'Rua Gonçalo Sousa 285',
      image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
      numberoftables: 10,
      capacity: 200,
      openingdays: 'Aberto de segunda a sábado',
      openinghours: '10:30',
      closinghours: '23:00',
    });

  expect(res.status).toBe(201);

  const { id } = res.body;
  const restaurantRegistrationDB = await app.services.restaurantregistration.find({ id });
  expect(restaurantRegistrationDB.password).not.toBeUndefined();
  expect(restaurantRegistrationDB.password).not.toBe('12345');
});

test('Test #4 - Inserir um registo de restaurante sem o nome do responsável do restaurante', () => {
  return request(app).post(route)
    .send({
      phone: 912345678,
      email: 'ruibarreto@gmail.com',
      password: '12345',
      name: 'La Gusto Italiano',
      category: 'Comida Italiana',
      desc: 'Restaurante de comida italiana situado em Braga',
      rphone: 253456789,
      location: 'Rua Gonçalo Sousa 285',
      image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
      numberoftables: 10,
      capacity: 200,
      openingdays: 'Aberto de segunda a sábado',
      openinghours: '10:30',
      closinghours: '23:00',
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Nome do responsável do restaurante obrigatório!');
    });
});

test('Test #5 - Inserir um registo de restaurantes sem o telemóvel do responsável do restaurante', () => {
  return request(app).post(route)
    .send({
      flname: 'Rui Barreto',
      email: 'ruibarreto@gmail.com',
      password: '12345',
      name: 'La Gusto Italiano',
      category: 'Comida Italiana',
      desc: 'Restaurante de comida italiana situado em Braga',
      rphone: 253456789,
      location: 'Rua Gonçalo Sousa 285',
      image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
      numberoftables: 10,
      capacity: 200,
      openingdays: 'Aberto de segunda a sábado',
      openinghours: '10:30',
      closinghours: '23:00',
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Telemóvel do responsável do restaurante obrigatório!');
    });
});

test('Test #6 - Inserir um registo de restaurantes sem o email do responsável do restaurante', () => {
  return request(app).post(route)
    .send({
      flname: 'Rui Barreto',
      phone: 912345678,
      password: '12345',
      name: 'La Gusto Italiano',
      category: 'Comida Italiana',
      desc: 'Restaurante de comida italiana situado em Braga',
      rphone: 253456789,
      location: 'Rua Gonçalo Sousa 285',
      image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
      numberoftables: 10,
      capacity: 200,
      openingdays: 'Aberto de segunda a sábado',
      openinghours: '10:30',
      closinghours: '23:00',
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Email do responsável do restaurante obrigatório!');
    });
});

test('Test #7 - Inserir um registo de restaurantes sem a password do responsável do restaurante', () => {
  return request(app).post(route)
    .send({
      flname: 'Rui Barreto',
      phone: 912345678,
      email: 'ruibarreto@gmail.com',
      name: 'La Gusto Italiano',
      category: 'Comida Italiana',
      desc: 'Restaurante de comida italiana situado em Braga',
      rphone: 253456789,
      location: 'Rua Gonçalo Sousa 285',
      image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
      numberoftables: 10,
      capacity: 200,
      openingdays: 'Aberto de segunda a sábado',
      openinghours: '10:30',
      closinghours: '23:00',
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Password do responsável do restaurante obrigatório!');
    });
});

test('Test #8 - Inserir um registo de restaurante sem o nome do restaurante', () => {
  return request(app).post(route)
    .send({
      flname: 'Rui Barreto',
      phone: 912345678,
      email: 'ruibarreto@gmail.com',
      password: '12345',
      category: 'Comida Italiana',
      desc: 'Restaurante de comida italiana situado em Braga',
      rphone: 253456789,
      location: 'Rua Gonçalo Sousa 285',
      image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
      numberoftables: 10,
      capacity: 200,
      openingdays: 'Aberto de segunda a sábado',
      openinghours: '10:30',
      closinghours: '23:00',
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Nome do restaurante é um atributo obrigatório!');
    });
});

test('Test #9 - Inserir um registo de restaurantes sem a categoria do restaurante', () => {
  return request(app).post(route)
    .send({
      flname: 'Rui Barreto',
      phone: 912345678,
      email: 'ruibarreto@gmail.com',
      password: '12345',
      name: 'La Gusto Italiano',
      desc: 'Restaurante de comida italiana situado em Braga',
      rphone: 253456789,
      location: 'Rua Gonçalo Sousa 285',
      image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
      numberoftables: 10,
      capacity: 200,
      openingdays: 'Aberto de segunda a sábado',
      openinghours: '10:30',
      closinghours: '23:00',
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Categoria do restaurante é um atributo obrigatório!');
    });
});

test('Test #10 - Inserir um registo de restaurantes sem a descrição do restaurante', () => {
  return request(app).post(route)
    .send({
      flname: 'Rui Barreto',
      phone: 912345678,
      email: 'ruibarreto@gmail.com',
      password: '12345',
      name: 'La Gusto Italiano',
      category: 'Comida Italiana',
      rphone: 253456789,
      location: 'Rua Gonçalo Sousa 285',
      image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
      numberoftables: 10,
      capacity: 200,
      openingdays: 'Aberto de segunda a sábado',
      openinghours: '10:30',
      closinghours: '23:00',
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Descrição do restaurante é um atributo obrigatório!');
    });
});

test('Test #11 - Inserir um registo de restaurantes sem o telefone do restaurante', () => {
  return request(app).post(route)
    .send({
      flname: 'Rui Barreto',
      phone: 912345678,
      email: 'ruibarreto@gmail.com',
      password: '12345',
      name: 'La Gusto Italiano',
      category: 'Comida Italiana',
      desc: 'Restaurante de comida italiana situado em Braga',
      location: 'Rua Gonçalo Sousa 285',
      image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
      numberoftables: 10,
      capacity: 200,
      openingdays: 'Aberto de segunda a sábado',
      openinghours: '10:30',
      closinghours: '23:00',
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Telefone do restaurante é um atributo obrigatório!');
    });
});

test('Test #12 - Inserir um registo de restaurantes sem a localização do restaurante', () => {
  return request(app).post(route)
    .send({
      flname: 'Rui Barreto',
      phone: 912345678,
      email: 'ruibarreto@gmail.com',
      password: '12345',
      name: 'La Gusto Italiano',
      category: 'Comida Italiana',
      desc: 'Restaurante de comida italiana situado em Braga',
      rphone: 253456789,
      image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
      numberoftables: 10,
      capacity: 200,
      openingdays: 'Aberto de segunda a sábado',
      openinghours: '10:30',
      closinghours: '23:00',
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Localização do restaurante é um atributo obrigatório!');
    });
});

test('Test #13 - Inserir um registo de restaurantes sem uma imagem do restaurante', () => {
  return request(app).post(route)
    .send({
      flname: 'Rui Barreto',
      phone: 912345678,
      email: 'ruibarreto@gmail.com',
      password: '12345',
      name: 'La Gusto Italiano',
      category: 'Comida Italiana',
      desc: 'Restaurante de comida italiana situado em Braga',
      rphone: 253456789,
      location: 'Rua Gonçalo Sousa 285',
      numberoftables: 10,
      capacity: 200,
      openingdays: 'Aberto de segunda a sábado',
      openinghours: '10:30',
      closinghours: '23:00',
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Imagem do restaurante é um atributo obrigatório!');
    });
});

test('Test #14 - Inserir um registo de restaurantes sem o número de mesas do restaurante', () => {
  return request(app).post(route)
    .send({
      flname: 'Rui Barreto',
      phone: 912345678,
      email: 'ruibarreto@gmail.com',
      password: '12345',
      name: 'La Gusto Italiano',
      category: 'Comida Italiana',
      desc: 'Restaurante de comida italiana situado em Braga',
      rphone: 253456789,
      location: 'Rua Gonçalo Sousa 285',
      image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
      capacity: 200,
      openingdays: 'Aberto de segunda a sábado',
      openinghours: '10:30',
      closinghours: '23:00',
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Número de mesas do restaurante é um atributo obrigatório!');
    });
});

test('Test #15 - Inserir um registo de restaurantes sem a capacidade do restaurante', () => {
  return request(app).post(route)
    .send({
      flname: 'Rui Barreto',
      phone: 912345678,
      email: 'ruibarreto@gmail.com',
      password: '12345',
      name: 'La Gusto Italiano',
      category: 'Comida Italiana',
      desc: 'Restaurante de comida italiana situado em Braga',
      rphone: 253456789,
      location: 'Rua Gonçalo Sousa 285',
      image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
      numberoftables: 10,
      openingdays: 'Aberto de segunda a sábado',
      openinghours: '10:30',
      closinghours: '23:00',
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Capacidade do restaurante é um atributo obrigatório!');
    });
});

test('Test #16 - Inserir um registo de restaurantes sem os dias de funcionamento', () => {
  return request(app).post(route)
    .send({
      flname: 'Rui Barreto',
      phone: 912345678,
      email: 'ruibarreto@gmail.com',
      password: '12345',
      name: 'La Gusto Italiano',
      category: 'Comida Italiana',
      desc: 'Restaurante de comida italiana situado em Braga',
      rphone: 253456789,
      location: 'Rua Gonçalo Sousa 285',
      image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
      numberoftables: 10,
      capacity: 200,
      openinghours: '10:30',
      closinghours: '23:00',
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Dias de funcionamento são um atributo obrigatório!');
    });
});

test('Test #17 - Inserir um registo de restaurantes sem as horas de abertura', () => {
  return request(app).post(route)
    .send({
      flname: 'Rui Barreto',
      phone: 912345678,
      email: 'ruibarreto@gmail.com',
      password: '12345',
      name: 'La Gusto Italiano',
      category: 'Comida Italiana',
      desc: 'Restaurante de comida italiana situado em Braga',
      rphone: 253456789,
      location: 'Rua Gonçalo Sousa 285',
      image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
      numberoftables: 10,
      capacity: 200,
      openingdays: 'Aberto de segunda a sábado',
      closinghours: '23:00',
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Horas de abertura são um atributo obrigatório!');
    });
});

test('Test #18 - Inserir um registo de restaurantes sem as horas de fecho', () => {
  return request(app).post(route)
    .send({
      flname: 'Rui Barreto',
      phone: 912345678,
      email: 'ruibarreto@gmail.com',
      password: '12345',
      name: 'La Gusto Italiano',
      category: 'Comida Italiana',
      desc: 'Restaurante de comida italiana situado em Braga',
      rphone: 253456789,
      location: 'Rua Gonçalo Sousa 285',
      image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
      numberoftables: 10,
      capacity: 200,
      openingdays: 'Aberto de segunda a sábado',
      openinghours: '10:30',
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Horas de fecho são um atributo obrigatório!');
    });
});

test('Test #19 - Inserir e confirmar a Palavra Passe', async () => {
  const registrationResponse = await request(app).post(route)
    .send({
      flname: 'Rui Barreto',
      phone: 912345678,
      email: 'ruibarreto@gmail.com',
      password: '12345',
      name: 'La Gusto Italiano',
      category: 'Comida Italiana',
      desc: 'Restaurante de comida italiana situado em Braga',
      rphone: 253456789,
      location: 'Rua Gonçalo Sousa 285',
      image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
      numberoftables: 10,
      capacity: 200,
      openingdays: 'Aberto de segunda a sábado',
      openinghours: '10:30',
      closinghours: '23:00',
    });

  expect(registrationResponse.status).toBe(201);

  const updatePasswordResponse = await request(app).put(`${route}/${registrationResponse.body.id}/updatepassword`)
    .send({
      newPassword: '54321',
      confirmNewPassword: '54321',
    });

  expect(updatePasswordResponse.status).toBe(200);
  expect(updatePasswordResponse.body.message).toBe('Palavra Passe atualizada com sucesso!');
});

test('Test #20 - Inserir Palavras Passes diferentes', async () => {
  const registrationResponse = await request(app).post(route)
    .send({
      flname: 'Rui Barreto',
      phone: 912345678,
      email: 'ruibarreto@gmail.com',
      password: '12345',
      name: 'La Gusto Italiano',
      category: 'Comida Italiana',
      desc: 'Restaurante de comida italiana situado em Braga',
      rphone: 253456789,
      location: 'Rua Gonçalo Sousa 285',
      image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
      numberoftables: 10,
      capacity: 200,
      openingdays: 'Aberto de segunda a sábado',
      openinghours: '10:30',
      closinghours: '23:00',
    });

  expect(registrationResponse.status).toBe(201);

  const updatePasswordResponse = await request(app).put(`${route}/${registrationResponse.body.id}/updatepassword`)
    .send({
      newPassword: '54321',
      confirmNewPassword: '4321',
    });

  expect(updatePasswordResponse.status).toBe(400);
  expect(updatePasswordResponse.body.error).toBe('A Palavra Passe deve ser igual nos dois campos!');
});

test('Test #21 - Atualizar dados de um registo de um restaurante', () => {
  return app.db('restaurantregistrations')
    .insert({
      flname: 'Rui Barreto',
      phone: 912345678,
      email: 'ruibarreto@gmail.com',
      password: '12345',
      name: 'La Gusto Italiano',
      category: 'Comida Italiana',
      desc: 'Restaurante de comida italiana situado em Braga',
      rphone: 253456789,
      location: 'Rua Gonçalo Sousa 285',
      image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
      numberoftables: 10,
      capacity: 200,
      openingdays: 'Aberto de segunda a sábado',
      openinghours: '10:30',
      closinghours: '23:00',
    }, ['id'])
    .then((restaurantRes) => request(app).put(`${route}/${restaurantRes[0].id}`)
      .send({
        flname: 'Rui Barreto',
        phone: 912345678,
        email: 'ruibarreto@gmail.com',
        password: '12345',
        name: 'Picanha Delight Grill',
        category: 'Comida de Picanha',
        desc: 'Restaurante de picanha situado em Braga',
        rphone: 253456789,
        location: 'Rua Gonçalo Sousa 285',
        image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
        numberoftables: 10,
        capacity: 150,
        openingdays: 'Aberto de segunda a sábado',
        openinghours: '11:30',
        closinghours: '23:30',
      }))
    .then((res) => {
      expect(res.status).toBe(200);
      expect(res.body.name).toBe('Picanha Delight Grill');
      expect(res.body.desc).toBe('Restaurante de picanha situado em Braga');
      expect(res.body).not.toHaveProperty('password');
    });
});
