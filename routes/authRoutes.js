const mongoose = require("mongoose");
const account = mongoose.model('accounts');

module.exports = app => 
{
    //routes...
    app.get('/account', async (request, response) =>
    {
        const {r_username, r_password} = request.query
    
        if(r_username == null || r_password == null)
        {
            response.send("Invalid!");
            return;
        }
    
        var userAccount = await account.findOne({username: r_username})
        
        if(userAccount == null)
        {
            console.log("Account creation part");
    
            var newAccount = new account
            ({
                username : r_username,
                password : r_password,
    
                lastAuthDate : Date.now()
            });
    
            await newAccount.save();
    
            response.send(newAccount);
            return;
        }
        else
        {
            if(r_password == userAccount.password)
            {
                userAccount.lastAuthDate = Date.now();
                await userAccount.save(); 
    
                //response.send("Authentication Success");
                //send back to user_page (current)
                response.send(userAccount + "Authentication Success");
                return;
            }
        }
    
        response.send("wrong password");
        return;
    });
}
