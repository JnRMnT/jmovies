import { Gulp } from "gulp";
const { exec } = require('child_process');
var runSequence = require('gulp4-run-sequence');

var gulp: Gulp = require("gulp");
var isDebug = true;

gulp.task('run-servers', function (cb) {
    exec('cd ../SolutionItems/BatchFiles && run-servers.bat', function (err, stdout, stderr) {
        console.log(stdout);
        console.log(stderr);
        cb(err); 
    });
});