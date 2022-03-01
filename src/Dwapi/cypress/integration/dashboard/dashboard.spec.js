before(function () {
    //something ...
    cy.visit("/#/dashboard", { timeout: 100000 });
});

describe("Dashboard", () => {

    const host=Cypress.env('host');
    const port=Cypress.env('port');
    const user=Cypress.env("user");
    const password = Cypress.env("password");
    /*it("Visit to the dashboard", () => {
        cy.visit("/#/dashboard");
    });*/

    it("Successfully setup the server", () => {
        cy.wait(10000);

        cy.get('[formcontrolName="server"]')
            .click()
            .focused()
            .clear()
            .type(`{selectall}${host}`);
    });
    it("Successfully setup the port", () => {
        cy.wait(2100);
        cy.get('[formcontrolName="port"]')
            .click()
            .focused()
            .clear()
            .type(`{selectall}${port}`);
    });

    it("Successfully setup the user", () => {
        cy.wait(2000);
        cy.get('[formcontrolName="user"]')
            .click()
            .focused()
            .clear()
            .type(`{selectall}${user}`);
    });

    it("Successfully setup the password", () => {
        cy.wait(2000);
        cy.get('[formcontrolName="password"]')
            .click()
            .focused()
            .clear()
            .type(`{selectall}${password}`);
    });

    it("successfully select Provider", () => {
        cy.wait(2000);
        cy.get('p-dropdown[formControlName="provider"]')
            .click()
            .contains("ul li > span", "MySQL")
            .click();
    });
    it("Successfully verify the database", () => {
        cy.wait(2000);
        cy.get('button[label = "Verify Database"]').find("span").click();
    });
    it("Successfully verify the server", () => {
        cy.wait(2000);
        cy.get('button[label = "Verify Server"]').find("span").click();
    });
    it("Successfully save the server settings", () => {
        cy.wait(2000);
        cy.get('button[label = "Save"]').find("span").click();
    });

    it("Validating the application version", () => {
        cy.request("GET", "/api/appDetails/version").then(function (response) {
            cy.log(response);
            expect(response.status).equal(200);
        });
    });

    it("Verify the application version", () => {
        cy.request("GET", "/api/appDetails/version").then(function (response) {
            cy.log(response);
            expect(response.status).equal(200);
        });
    });

    it("verify the server", () => {
        cy.request({
            method: "POST",
            url: "/api/wizard/verifyserver",
            body: {
                database: Cypress.env("database"),
                password: Cypress.env("password"),
                port: Cypress.env("port"),
                provider: 1,
                server: Cypress.env("host"),
                user: Cypress.env("user"),
            },
            failOnStatusCode: false,
        }).then((res) => {
           // expect(res.status).to.be.gt(299);

            if (res.status == 500) {
                throw new Error(
                    `Server responded tp with ${res.status} and error is ${res.body}`
                );
            }
            // throw new Error(`Server responded to  with ${res.status}`);

            //expect(res.status).equal(200);

            cy.log(res.body);
        });
    });

    it("verify the Database", () => {
        cy.request({
            method: "POST",
            url: "/api/wizard/verifydb",
            body: {
                database: Cypress.env("database"),
                password: Cypress.env("password"),
                port: Cypress.env("port"),
                provider: 1,
                server: Cypress.env("host"),
                user: Cypress.env("user"),
            },
            failOnStatusCode: false,
        }).then((res) => {
           // expect(res.status).to.be.gt(299);
            // throw new Error(`Server responded to  with ${res.status}`);

            if (res.status == 500) {
                throw new Error(
                    `Server responded  with ${res.status} and error is ${res.body}`
                );
            }

            //expect(res.status).equal(200);

            cy.log(res.body);
        });
    });
});
