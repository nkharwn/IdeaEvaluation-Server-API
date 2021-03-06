# Idea Evaluation API

### Technology Stack
* .Net Core 3.1
* SQLite database with Entity Framework Core Database first approach

### Api Endpoints
* Post:- http://localhost:5000/api/login
* Get:- http://localhost:5000/api/idea/{UserId}
* Post:- http://localhost:5000/api/evaluate

### Approach to assign ideas to user for Evaluation

#### Database Design
##### Tables
 * User Table  columns (UserId(PK), UserName, Password).
 * Idea Table columns (IdeaId(PK), IdeaName, Description).
 * IdeaEvaluationHistory Table columns (IdeaEvaluationId(PK), UserId(FK), IdeaId(FK)).
#### Bussiness Logic
* Calculate total number of ideas that need to be evaluated .
* Find out available ideas to be evaluated based evaluted count which is less than or equal to 3 based on IdeaEvaluationHistory table .
* Then Calculate available users (Evaluators).
* Find out the how much idea can be evaluted by user based on total number of ideas and total number of users.
* Assign ideas from available ideas to the current logged in evaluator and make sure that same evaluated idea is not assigned again to the same evaluator.
##### Scenarios covered
*  Validate user based on username and password.
*  If new idea added at runtime it will be randomly assigned to any user
*  If user evaluate any Idea he can not evaluate again same idea.
*  Handled case in which no idea is left for evaluation.
 


