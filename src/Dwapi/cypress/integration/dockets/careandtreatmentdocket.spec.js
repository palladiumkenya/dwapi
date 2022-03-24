const { doesNotThrow } = require("assert");

Cypress.on("uncaught:exception", (err, runnable) => {
    // returning false here prevents Cypress from
    // failing the test
    return false;
});
before(function () {
    //something ...
    cy.visit("/#/datawarehouse", { timeout: 100000 });
});

it("Load from the EMR", () => {
    cy.wait(3000);
    cy.contains("Load from EMR").click({ force: true });

    cy.intercept("GET", "/api/DwhExtracts/status/**", (req) => {
        console.log(req);
    }).as("status");

    cy.intercept("POST", "/api/DwhExtracts/extractAll", {
        timeout: 100000,
    }).then(function (response) {
        cy.log(response);
        //expect(response.status).equal(200);
        //expect(response.body).to.not.be.null;
        console.log(response);
    });

    cy.wait("@status", { timeout: 100000 })
        .its("response.statusCode")
        .should("equal", 200);
});
