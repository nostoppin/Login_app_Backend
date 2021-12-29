//ACCOUNT MODEL

const mongoose = require('mongoose');

const { Schema } = mongoose;

const accountSchema = new Schema
({
    username: String,
    password: String,

    lastAuthDate: Date,
});

mongoose.model('accounts', accountSchema);