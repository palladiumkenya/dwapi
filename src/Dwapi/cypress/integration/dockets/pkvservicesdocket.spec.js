const { doesNotThrow } = require("assert");

Cypress.on("uncaught:exception", (err, runnable) => {
    // returning false here prevents Cypress from
    // failing the test
    return false;
});
before(function () {
    //something ...
    cy.visit("/#/cbs", { timeout: 100000 });
});

function getXhrRequests() {
    let xhrRequests = [];
    cy.intercept("GET", "/api/cbs/status/**", (req) => {
        xhrRequests.push(req);
    }).as("status");

    return xhrRequests;
}
it("Load from the EMR PKV services data", () => {
   cy.wait(3000);
    cy.contains("Load from EMR").click({ force: true });
     cy.request("GET", "/api/Cbs/count").then(function (response) {
         cy.log(response);
         expect(response.status).equal(200);
         expect(response.body).to.not.be.null;
         console.log(response);

     });






    /* cy.wait("@status", {
        requestTimeout: 1000000,
        defaultCommandTimeout: 1000000,
    });*/
});
