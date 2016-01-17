var ViewModel = function () {
    var self = this;
    self.jobs = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();
    self.newJob = {
        Url: ko.observable()
    }

    var jobsUrl = '/api/jobs/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllJobs() {
        ajaxHelper(jobsUrl, 'GET').done(function (data) {
            self.jobs(data);
        });
    }

    self.getJobDetail = function (item) {
        ajaxHelper(jobsUrl + item.Id, 'GET').done(function (data) {
            self.detail(data);
        });
    }

    self.addJob = function (formElement) {
        var job = {
            Url: self.newJob.Url()
        };

        ajaxHelper(jobsUrl, 'POST', job).done(function (item) {
            self.jobs.push(item);
        });
    }

    // Fetch the initial data.
    getAllJobs();

    window.setInterval(getAllJobs, 5000);
};

ko.applyBindings(new ViewModel());
