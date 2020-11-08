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
* Calculate total number of ideas that need to be evaluated based evaluted count which is less than or equal to 3 based on IdeaEvaluationHistory table .
* Then Calculate available users (Evaluators).
* Equally divide ideas amongst evaluators and make sure that same evaluated idea is not assigned again to the same evaluator.

