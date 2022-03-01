before(function () {
    cy.visit("/#/registry/CBS", { timeout: 100000 });
});

describe("PKV Services configuration", () => {
    it("Edit the URL registry", () => {
        cy.xpath('//*[@id="url"]')
            .clear()
            .type("https://auth.kenyahmis.org:6763");
    });

    it("Verify the url Registry", () => {
        cy.xpath(
            "/html/body/liveapp-root/div/div/div[2]/div[1]/liveapp-registry-manager/div/div/div[2]/liveapp-registry-config/div/form/div/div[5]/div[2]/button"
        ).click();
    });
});
