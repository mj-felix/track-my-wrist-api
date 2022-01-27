OK, long story short...

The goal (as an engineer) is to make your code highly readable and maintainable so that others can work in your code with you, and to do that you have to bake in your own language and abstractions that are tailored to the domain of your software. why, becuase that's all you want to be coding, nothing else.

That is very hard to do when you accept the artificial constraints and complications given to you by ASP.NET examples and the things that are made available to you by the libraries and frameworks you are using.
such as those of:

- ASP.NET Core itself
- ASP.NET MVC controllers,
- Entity Framework (ORM)
- etc, etc

Yes, these are what all the teaching examples use, and that is what you see the most of, but they suck when you don't build your own abstractions on top of them. Why, because they are all focused on showing how to get shit done the easiest (least code written) way. That's not what you want to learn my friend.

The problem is that your abstractions (as they are today) are technology-abstractions (defined by those libraries/frameworks), not domain-abstractions defined by you and your project, and the problems you are solving. You need to rise above and out of the mud and build a better landscape to live in.

So, unless you are at expert level with these technologies, you are going to be a slave to them. and that produces sucky-ass code, where you are going to get bogged down in sucky-ass abstractions!
Instead, you need to build an abstraction that you like, and fit the libraries and frameworks to that. then you create your own language that reads the way you like it to read. and then you can deal with the things you want to in your own abstractions.

OK, that's theory.

Lets take the first step. Yes, lets use ASP.NET code, and ASP.NET MVC (as infrastructure), but lets build on them some better abstractions that you can live within.

You know that an HTTP request comes in as your API. and you now know that we want that separated from our application and domain. Well, because Clean Architecture (and Jezz!) tells you to :slightly_smiling_face: - lets make that assumption at least.

This means that your external programming interface, is your WebAPI (HTTP/REST), and your internal programming interface is dictated by your Application Layer, and that means that the code in the controllers must CONFORM to your application interface. and then we will build an explicit domain layer that is separate from your application layer, otherwise you'll be a slave to your database. Trust me on that.

So, keep your Controllers, but delegate everything to a new application layer (`IApplicationWhatever`). Similar to your `IWatchService` but its no longer a "service". It is, by definition your new application (the thing you are building that does Watch Wearing Habit stuff), that just happens to be exposed over HTTP (for now at least).

Your Controllers should do these things (Separation of concerns/responsibilities) and nothing more:

1. Convert HTTP paths, QueryString and JSON bodies to DTO's and (logical) Commands.
1. Ensure that the Authorization header contains your JWT identifying the caller, and that it is legitimate (digital signature).
1. Convert the JWT claims (from that JWT) to some object (that you like the name of) identifying the caller and the context of the call. (much like you saw in ICurrentCaller yesterday).
1. Perform basic/coarse RBAC assertions on the caller at this point in time. Authenticated or NOT.
1. Convert the DTO return values from the Application layer commands and queries into JSON/CSV/XML responses.
1. Handle any exceptions thrown from the Application Layer into HTTP status codes and descriptions.

You can centralise points #1, #2, #3 and #5 in ASP.NET Filters and centralised code (in that layer). You can manually deconstruct or map #4 in the controller code directly.

Your controller code should now be trivial, and have no other rules in it. Push all those rules into the Application Layer.

This will make your Controllers trivial to write (that's the way we want them - a fine detail), and take your attention away form them. You wont even unit test them. There will be nothing to test here.

Your focus then moves to the Application Layer, and Domain layer where the interesting stuff happens.

That is step 1. Put everything in the Application first.

Step 2, is to define a Domain layer, and get rid of the dependency on your database and your ORM. Because that is the next suck-ass thing to be a slave to!

The other thing that does not help you, is defining layers in the same assembly.

You can do this if you are expert (and highly disciplined), but there is nothing (hard) to enforce the rules of your constraints if you do this.

You need to build _hard_ barriers to stop yourself violating these constraints, to write the simplest code.

For example, at this point in time, would split my codebase into at least 4x assemblies:

1. containing your application DTO's and application interfaces, and repository interfaces
1. containing your webhost and controllers and DI code, filters etc, and their inbound and outbound requests and response objects.
1. containing your application classes
1. containing your repositories

then:

- 2 has a reference to 1, 3 and 4
- 3 has a reference to 1
- 4 has a reference to 1

Now we have dependencies in the right direction.
