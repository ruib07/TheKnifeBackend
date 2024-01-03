exports.up = (knex) => {
  return knex.schema.createTable('reservationhistoryrestaurants', (t) => {
    t.integer('reservationhistory_id')
      .references('id')
      .inTable('reservationhistory').notNull();
    t.integer('restaurant_id')
      .references('id')
      .inTable('restaurants').notNull();
  });
};

exports.down = (knex) => {
  return knex.schema.dropTable('reservationhistoryrestaurants');
};
