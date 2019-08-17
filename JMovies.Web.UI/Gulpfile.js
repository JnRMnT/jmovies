"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var exec = require('child_process').exec;
var runSequence = require('gulp4-run-sequence');
var gulp = require("gulp");
var isDebug = true;
gulp.task('run-servers', function (cb) {
    exec('cd ../SolutionItems/BatchFiles && run-servers.bat', function (err, stdout, stderr) {
        console.log(stdout);
        console.log(stderr);
        cb(err);
    });
});
//# sourceMappingURL=Gulpfile.js.map